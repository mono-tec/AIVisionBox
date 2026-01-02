#include "pch.h"
#include "AivbOpenCvNative.h"
#include "OpenCvCardDiamondCounter.h"
#include <opencv2/core.hpp>

AivbCountResult Aivb_Ping()
{
    cv::Mat m(10, 10, CV_8UC1);
    AivbCountResult r{};
    r.isOk = (m.rows == 10) ? 1 : 0;
    r.objectCount = 0;
    r.errorCode = 0;
    return r;
}

AivbCountResult Aivb_CountDiamonds_Bgr24(
    const unsigned char* bgr, int width, int height, int strideBytes,
    AivbRoiRect roi, int minArea, int maxArea)
{
    AivbCountResult r{};
    r.isOk = 0;
    r.objectCount = 0;
    r.errorCode = 0; // ★明示

    // --- 引数チェック（あなたの方針でOK）
    if (!bgr) { r.errorCode = 1; return r; }
    if (width <= 0 || height <= 0) { r.errorCode = 1; return r; }
    if (strideBytes < width * 3) { r.errorCode = 1; return r; }
    if (roi.w <= 0 || roi.h <= 0) { r.errorCode = 1; return r; }
    if (roi.x < 0 || roi.y < 0) { r.errorCode = 1; return r; }
    if (roi.x + roi.w > width || roi.y + roi.h > height) { r.errorCode = 1; return r; }
    if (minArea < 0 || maxArea < 0 || minArea > maxArea) { r.errorCode = 1; return r; }

    // ★ここが本体：OpenCV処理に委譲して、その結果をそのまま返す
    return OpenCvCardDiamondCounter::CountBgr24(
        bgr, width, height, strideBytes,
        roi, minArea, maxArea
    );
}


extern "C" {

    AIVB_API int Aivb_OpenCv_DefaultMinArea()
    {
        return OpenCvCardDiamondCounter::DefaultMinArea;
    }

    AIVB_API int Aivb_OpenCv_DefaultMaxArea()
    {
        return OpenCvCardDiamondCounter::DefaultMaxArea;
    }

}