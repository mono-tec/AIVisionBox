#include <iostream>
#include <opencv2/imgcodecs.hpp>
#include "AivbOpenCvNative.h"

int main()
{
    // assets/opencv/cards.png を読み込み
    auto img = cv::imread("assets/opencv/cards.png");
    if (img.empty()) {
        std::cout << "failed to load cards.png\n";
        return 1;
    }

    AivbRoiRect roi{ 0, 0, 0, 0 }; // 0なら全体
    auto r = Aivb_CountDiamonds_Bgr24(
        img.data, img.cols, img.rows, (int)img.step,
        roi,
        50,   // minArea（後で調整）
        5000  // maxArea（後で調整）
    );

    std::cout << "count=" << r.objectCount << " ok=" << r.isOk << " err=" << r.errorCode << "\n";
    return 0;
}
