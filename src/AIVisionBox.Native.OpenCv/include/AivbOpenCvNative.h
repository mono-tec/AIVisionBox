#pragma once
/*
 * AIVisionBox.Native.OpenCv
 *
 * Copyright (c) 2026 mono-tec
 *
 * ---------------------------------------------------------------------------
 * Overview:
 *   Native OpenCV image processing engine for the AIVisionBox project.
 *
 *   This header defines the public C API exported by the native OpenCV module.
 *   The API is designed to be used from multiple environments such as:
 *     - C# (P/Invoke)
 *     - Web / HTTP based inference services
 *     - Embedded Linux systems (e.g. Renesas RZ/V series)
 *
 * Design Policy:
 *   - Provide a stable C ABI using `extern "C"`
 *   - Avoid C++ specific types in public interfaces
 *   - Separate image processing logic from UI / control logic
 *   - Enable future cross-platform builds (Windows / Linux)
 *
 * Scope:
 *   - This module is responsible ONLY for image processing.
 *   - AI inference, UI, PLC control, and communication are handled
 *     by upper layers of the AIVisionBox architecture.
 *
 * Notes:
 *   - This API is currently under development and may change.
 *   - When used in commercial products, ensure compliance with
 *     the OpenCV license.
 *
 * ---------------------------------------------------------------------------
 * This file is part of the AIVisionBox project.
 */


#include "AivbTypes.h"

#ifdef _WIN32
#ifdef AIVB_OPENCV_NATIVE_EXPORTS
#define AIVB_API __declspec(dllexport)
#else
#define AIVB_API __declspec(dllimport)
#endif
#else
#define AIVB_API
#endif

#ifdef __cplusplus
extern "C" {
#endif

    /**
     * @brief Native DLL の生存確認・疎通確認用 API
     *
     * OpenCV が正しくリンクされ、DLL が動作しているかを
     * 確認するための簡易関数。
     *
     * @return AivbCountResult
     *         - errorCode == 0 : 正常
     */
    AIVB_API AivbCountResult Aivb_Ping(void);

    /**
     * @brief BGR24 画像からトランプ（ダイヤ）の個数をカウントする
     *
     * OpenCV による画像処理を行い、指定 ROI 内の
     * ダイヤマークを検出・個数カウントする。
     *
     * @param bgr         BGR24 形式の画像バッファ（先頭ポインタ）
     * @param width       画像幅（ピクセル）
     * @param height      画像高さ（ピクセル）
     * @param strideBytes 1 行あたりのバイト数
     * @param roi         処理対象の ROI（画像座標）
     * @param minArea     検出対象の最小面積
     * @param maxArea     検出対象の最大面積
     *
     * @return AivbCountResult
     */
    AIVB_API AivbCountResult Aivb_CountDiamonds_Bgr24(
        const unsigned char* bgr,
        int width,
        int height,
        int strideBytes,
        AivbRoiRect roi,
        int minArea,
        int maxArea
    );

#ifdef __cplusplus
} // extern "C"
#endif