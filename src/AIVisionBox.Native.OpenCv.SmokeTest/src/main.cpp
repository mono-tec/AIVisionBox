#include "AivbOpenCvNative.h"
#include "TestEnv.h"
#include <opencv2/imgcodecs.hpp>
#include <filesystem>
#include <iostream>


int main()
{
    auto path = GetOpenCvCardsPngPath();
    std::cout << "exists(" << path << ")=" << std::filesystem::exists(path) << "\n";
    auto img = cv::imread(path.string(), cv::IMREAD_COLOR);
    if (img.empty())
    {
        std::cout << "failed to load cards.png\n";
        return 1;
    }

    //AivbRoiRect roi{ 0, 0, img.cols, img.rows };
    //AivbRoiRect roi{ 50, 50, 300, 400 }; // x,y,w,h
    AivbRoiRect roi{ 90, 120, 220, 260 };

    //int minArea = Aivb_OpenCv_DefaultMinArea();
    //int maxArea = Aivb_OpenCv_DefaultMaxArea();
    int minArea = 1200;
    int maxArea = 20000;

    AivbCountResult r = Aivb_CountDiamonds_Bgr24(
        img.data,
        img.cols,
        img.rows,
        (int)img.step,
        roi,
        minArea,
        maxArea
    );

    std::cout << "ok=" << r.isOk
        << " err=" << r.errorCode
        << " count=" << r.objectCount
        << "\n";

    return (r.errorCode == 0) ? 0 : 2;
}
