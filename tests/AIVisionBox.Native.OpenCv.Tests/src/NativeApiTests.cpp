#include "pch.h"
#include <gtest/gtest.h>
#include "AivbOpenCvNative.h"

// A: API契約テスト（入力が変でも落ちずに戻ることを保証）

TEST(NativeOpenCv_ApiContract, Ping_ReturnsOk)
{
    AivbCountResult r = Aivb_Ping();
    EXPECT_EQ(r.errorCode, 0);
}

TEST(NativeOpenCv_ApiContract, CountDiamonds_NullBuffer_ReturnsError)
{
    AivbRoiRect roi{ 0, 0, 10, 10 };

    AivbCountResult r = Aivb_CountDiamonds_Bgr24(
        nullptr,
        10, 10,
        10 * 3,
        roi,
        10,
        1000
    );

    EXPECT_NE(r.errorCode, 0);
    EXPECT_EQ(r.isOk, 0);
}

TEST(NativeOpenCv_ApiContract, CountDiamonds_InvalidWidthHeight_ReturnsError)
{
    unsigned char dummy[30] = { 0 };
    AivbRoiRect roi{ 0, 0, 10, 10 };

    // width=0
    {
        AivbCountResult r = Aivb_CountDiamonds_Bgr24(
            dummy,
            0, 10,
            10 * 3,
            roi,
            10,
            1000
        );
        EXPECT_NE(r.errorCode, 0);
        EXPECT_EQ(r.isOk, 0);
    }

    // height=0
    {
        AivbCountResult r = Aivb_CountDiamonds_Bgr24(
            dummy,
            10, 0,
            10 * 3,
            roi,
            10,
            1000
        );
        EXPECT_NE(r.errorCode, 0);
        EXPECT_EQ(r.isOk, 0);
    }
}

TEST(NativeOpenCv_ApiContract, CountDiamonds_InvalidStride_ReturnsError)
{
    // BGR24 なら最小 stride は width*3 が基本
    unsigned char dummy[300] = { 0 };
    AivbRoiRect roi{ 0, 0, 10, 10 };

    AivbCountResult r = Aivb_CountDiamonds_Bgr24(
        dummy,
        10, 10,
        1, // 明らかに不正
        roi,
        10,
        1000
    );

    EXPECT_NE(r.errorCode, 0);
    EXPECT_EQ(r.isOk, 0);
}

TEST(NativeOpenCv_ApiContract, CountDiamonds_InvalidAreaParams_ReturnsError)
{
    unsigned char dummy[300] = { 0 };
    AivbRoiRect roi{ 0, 0, 10, 10 };

    // minArea > maxArea は不正扱いを推奨
    AivbCountResult r = Aivb_CountDiamonds_Bgr24(
        dummy,
        10, 10,
        10 * 3,
        roi,
        5000, // min
        1000  // max
    );

    EXPECT_NE(r.errorCode, 0);
    EXPECT_EQ(r.isOk, 0);
}

TEST(NativeOpenCv_ApiContract, CountDiamonds_RoiOutside_ReturnsError)
{
    unsigned char dummy[300] = { 0 };

    // 画像(10x10)に対して完全に外
    AivbRoiRect roi{ 100, 100, 10, 10 };

    AivbCountResult r = Aivb_CountDiamonds_Bgr24(
        dummy,
        10, 10,
        10 * 3,
        roi,
        10,
        1000
    );

    EXPECT_NE(r.errorCode, 0);
    EXPECT_EQ(r.isOk, 0);
}

TEST(NativeOpenCv_ApiContract, CountDiamonds_RoiZeroOrNegative_ReturnsError)
{
    unsigned char dummy[300] = { 0 };

    // w=0
    {
        AivbRoiRect roi{ 0, 0, 0, 10 };
        AivbCountResult r = Aivb_CountDiamonds_Bgr24(
            dummy, 10, 10, 10 * 3, roi, 10, 1000
        );
        EXPECT_NE(r.errorCode, 0);
        EXPECT_EQ(r.isOk, 0);
    }

    // h=0
    {
        AivbRoiRect roi{ 0, 0, 10, 0 };
        AivbCountResult r = Aivb_CountDiamonds_Bgr24(
            dummy, 10, 10, 10 * 3, roi, 10, 1000
        );
        EXPECT_NE(r.errorCode, 0);
        EXPECT_EQ(r.isOk, 0);
    }
}
