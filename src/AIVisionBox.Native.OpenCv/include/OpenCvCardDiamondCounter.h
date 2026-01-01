#pragma once
#include "AivbTypes.h"

class OpenCvCardDiamondCounter
{
public:
    static AivbCountResult CountBgr24(
        const unsigned char* bgr,
        int width, int height, int strideBytes,
        AivbRoiRect roi,
        int minArea, int maxArea);
};
