#include "AivbOpenCvNative.h"
#include "OpenCvCardDiamondCounter.h"
#include <opencv2/imgproc.hpp>
#include <opencv2/core.hpp>
#include <opencv2/opencv.hpp>


static cv::Rect ClampRoi(const AivbRoiRect& roi, int width, int height)
{
    if (roi.w <= 0 || roi.h <= 0) return cv::Rect(0, 0, width, height);

    int x = std::max(0, roi.x);
    int y = std::max(0, roi.y);
    int w = std::max(0, roi.w);
    int h = std::max(0, roi.h);

    if (x >= width || y >= height) return cv::Rect(0, 0, width, height);

    int x2 = std::min(width, x + w);
    int y2 = std::min(height, y + h);

    if (x2 <= x || y2 <= y) return cv::Rect(0, 0, width, height);

    return cv::Rect(x, y, x2 - x, y2 - y);
}

AivbCountResult OpenCvCardDiamondCounter::CountBgr24(
    const unsigned char* bgr,
    int width,
    int height,
    int strideBytes,
    AivbRoiRect roi,
    int minArea,
    int maxArea)
{
    AivbCountResult r{};
    r.isOk = 0;
    r.objectCount = 0;
    r.errorCode = 0;

    // --- validation
    if (!bgr || width <= 0 || height <= 0 || strideBytes < width * 3)
    {
        r.errorCode = 1; // invalid args
        return r;
    }
    if (minArea <= 0) minArea = 50;
    if (maxArea <= 0) maxArea = 5000;

    try
    {
        // 1) Wrap input (BGR, 8UC3)
        cv::Mat src(height, width, CV_8UC3, const_cast<unsigned char*>(bgr), (size_t)strideBytes);

        // 2) ROI crop
        cv::Rect rect = ClampRoi(roi, width, height);
        cv::Mat roiMat = src(rect).clone(); // cloneしておくと後処理が安全

        // 3) HSV へ
        cv::Mat hsv;
        cv::cvtColor(roiMat, hsv, cv::COLOR_BGR2HSV);

        // 4) 赤抽出（HSVは赤が0付近と180付近に分かれるので2レンジ）
        cv::Mat mask1, mask2, mask;
        // ここは調整ポイント：cards.png想定の"赤"
        cv::inRange(hsv, cv::Scalar(0, 80, 80), cv::Scalar(10, 255, 255), mask1);
        cv::inRange(hsv, cv::Scalar(170, 80, 80), cv::Scalar(180, 255, 255), mask2);
        mask = mask1 | mask2;

        int nz = cv::countNonZero(mask);
        std::cout << "[dbg] mask nonzero=" << nz << " / (w*h=" << mask.cols * mask.rows << ")\n";

        // 5) ノイズ除去（開閉）
        //cv::Mat kernel = cv::getStructuringElement(cv::MORPH_ELLIPSE, cv::Size(3, 3));
        //cv::morphologyEx(mask, mask, cv::MORPH_OPEN, kernel, cv::Point(-1, -1), 1);
        //cv::morphologyEx(mask, mask, cv::MORPH_CLOSE, kernel, cv::Point(-1, -1), 1);
        
        // NOTE: cards.png では「赤い領域が分割されて輪郭が増えやすい」ので CLOSE を強める
        cv::Mat kernelOpen = cv::getStructuringElement(cv::MORPH_ELLIPSE, cv::Size(3, 3));
        cv::Mat kernelClose = cv::getStructuringElement(cv::MORPH_ELLIPSE, cv::Size(5, 5));

        // 小ノイズ除去（OPEN）
        cv::morphologyEx(mask, mask, cv::MORPH_OPEN, kernelOpen, cv::Point(-1, -1), 1);

        // 分割された赤領域を結合（CLOSEを強める）
        cv::morphologyEx(mask, mask, cv::MORPH_CLOSE, kernelClose, cv::Point(-1, -1), 2);

        // (4) debug: mask保存
        cv::imwrite("debug_mask.png", mask);

        // 6) 輪郭抽出
        std::vector<std::vector<cv::Point>> contours;
        cv::findContours(mask, contours, cv::RETR_EXTERNAL, cv::CHAIN_APPROX_SIMPLE);

        // (4) debug: contours保存
        cv::Mat vis;
        cv::cvtColor(mask, vis, cv::COLOR_GRAY2BGR);
        cv::drawContours(vis, contours, -1, cv::Scalar(0, 255, 0), 2);
        cv::imwrite("debug_contours.png", vis);

        int count = 0;
        for (const auto& c : contours)
        {
            double area = cv::contourArea(c);
            if (area < minArea) continue;
            if (area > maxArea) continue;
            count++;
        }

        std::cout << "[dbg] contours=" << contours.size() << "\n";

        double minA = 1e18, maxA = 0;
        for (auto& c : contours) {
            double a = cv::contourArea(c);
            minA = std::min(minA, a);
            maxA = std::max(maxA, a);
        }
        if (!contours.empty())
            std::cout << "[dbg] area range: " << minA << " .. " << maxA
            << " (filter " << minArea << " .. " << maxArea << ")\n";

        r.isOk = 1;
        r.objectCount = count;
        r.errorCode = 0;
        return r;
    }
    catch (...)
    {
        r.errorCode = 2; // processing error
        return r;
    }
}