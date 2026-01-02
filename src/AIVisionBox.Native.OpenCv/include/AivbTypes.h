#pragma once

/*
 * AIVisionBox.Native.OpenCv
 *
 * Copyright (c) 2026 mono-tec
 *
 * Description:
 *   Native OpenCV image processing engine for AIVisionBox.
 *   Provides C API for cross-language usage (C#, etc.).
 *
 * Note:
 *   This file is part of the AIVisionBox project.
 */


struct AivbRoiRect { int x; int y; int w; int h; };

struct AivbCountResult {
    int isOk;        // 1:OK, 0:NG
    int objectCount; // count
    int errorCode;   // 0:OK, other: error
};
