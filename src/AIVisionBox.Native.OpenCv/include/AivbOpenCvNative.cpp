#define AIVB_OPENCV_NATIVE_EXPORTS
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
    return OpenCvCardDiamondCounter::CountBgr24(
        bgr, width, height, strideBytes, roi, minArea, maxArea);
}
