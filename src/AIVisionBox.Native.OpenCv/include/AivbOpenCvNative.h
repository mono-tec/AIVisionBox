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

#ifdef AIVB_OPENCV_NATIVE_EXPORTS
#define AIVB_API __declspec(dllexport)
#else
#define AIVB_API __declspec(dllimport)
#endif

extern "C" {

    AIVB_API AivbCountResult Aivb_Ping();

    AIVB_API AivbCountResult Aivb_CountDiamonds_Bgr24(
        const unsigned char* bgr,
        int width,
        int height,
        int strideBytes,
        AivbRoiRect roi,
        int minArea,
        int maxArea
    );

}
