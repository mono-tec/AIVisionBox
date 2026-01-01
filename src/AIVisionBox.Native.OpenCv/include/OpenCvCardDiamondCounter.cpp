#include "OpenCvCardDiamondCounter.h"
#include <opencv2/imgproc.hpp>
#include <opencv2/core.hpp>

static cv::Rect ToCvRect(AivbRoiRect roi, int width, int height)
{
    int x = std::max(0, roi.x);
    int y = std::max(0, roi.y);
    int w = std::max(0, roi.w);
    int h = std::max(0, roi.h);
    if (w == 0 || h == 0) return cv::Rect(0, 0, width, height);

    // clamp
    if (x + w > width)  w = width - x;
    if (y + h > height) h = height - y;
    if (w <= 0 || h <= 0) return cv::Rect(0, 0, width, height);

    return cv::Rect(x, y, w, h);
}

AivbCountResult OpenCvCardDiamondCounter::CountBgr24(
    const unsigned char* bgr,
    int width, int height, int strideBytes,
    AivbRoiRect roi,
    int minArea, int maxArea)
{
    AivbCountResult r{};
    r.isOk = 0;
    r.objectCount = 0;
    r.errorCode = 0;

    if (!bgr || width <= 0 || height <= 0 || strideBytes < width * 3) {
        r.errorCode = 1; // invalid args
        return r;
    }

    // 1) Mat化（BGR）
    cv::Mat src(height, width, CV_8UC3, const_cast<unsigned char*>(bgr), strideBytes);

    // 2) ROI
    cv::Rect rc = ToCvRect(roi, width, height);
    cv::Mat roiMat = src(rc).clone(); // cloneで連続化＆安全化

    // 3) HSV変換
    cv::Mat hsv;
    cv::cvtColor(roiMat, hsv, cv::COLOR_BGR2HSV);

    // 4) 赤色抽出（赤は 0付近 と 180付近に分かれる）
    cv::Mat mask1, mask2, mask;
    // ※閾値は後で調整。まず動く値。
    cv::inRange(hsv, cv::Scalar(0, 70, 50), cv::Scalar(10, 255, 255), mask1);
    cv::inRange(hsv, cv::Scalar(170, 70, 50), cv::Scalar(180, 255, 255), mask2);
    mask = mask1 | mask2;

    // 5) ノイズ除去（軽く）
    cv::Mat kernel = cv::getStructuringElement(cv::MORPH_ELLIPSE, cv::Size(3, 3));
    cv::morphologyEx(mask, mask, cv::MORPH_OPEN, kernel, cv::Point(-1, -1), 1);
    cv::morphologyEx(mask, mask, cv::MORPH_CLOSE, kernel, cv::Point(-1, -1), 1);

    // 6) 輪郭抽出
    std::vector<std::vector<cv::Point>> contours;
    cv::findContours(mask, contours, cv::RETR_EXTERNAL, cv::CHAIN_APPROX_SIMPLE);

    // 7) 面積フィルタ
    int count = 0;
    if (minArea <= 0) minArea = 30; // 適当なデフォルト
    if (maxArea <= 0) maxArea = 1000000;

    for (const auto& c : contours) {
        double a = cv::contourArea(c);
        if (a >= minArea && a <= maxArea) {
            count++;
        }
    }

    r.isOk = 1;
    r.objectCount = count;
    r.errorCode = 0;
    return r;
}
