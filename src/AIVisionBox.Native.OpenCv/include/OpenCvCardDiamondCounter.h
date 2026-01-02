#pragma once
#include "AivbTypes.h"

/// <summary>
/// cards.png（OpenCVサンプル）を題材に、BGR画像から「ダイヤ（赤）」マークの個数を数えるための
/// 内部実装クラス。
///
/// 注意:
/// - 本クラスは DLL公開API ではない（AivbOpenCvNative.* から呼ばれる内部実装）。
/// - DLL境界のABIを安定させるため、外部公開は C API（AivbOpenCvNative.h）に限定する。
/// </summary>
class OpenCvCardDiamondCounter
{
public:
    /// <summary>
    /// BGR24（CV_8UC3）画像からダイヤの個数を推定する。
    ///
    /// 入力:
    /// - bgr        : BGR24の先頭ポインタ（row-major）
    /// - width      : 画像幅（px）
    /// - height     : 画像高さ（px）
    /// - strideBytes: 1行あたりのバイト数（通常は width*3 以上）
    /// - roi        : 解析対象領域。roi.w<=0 または roi.h<=0 の場合は「画像全体」を意味する。
    /// - minArea    : 輪郭（ブロブ）面積の下限。<=0 の場合は既定値を内部で使用する。
    /// - maxArea    : 輪郭（ブロブ）面積の上限。<=0 の場合は既定値を内部で使用する。
    ///
    /// 出力:
    /// - AivbCountResult:
    ///     - errorCode: 0=成功, 1=引数不正, 2=OpenCV処理失敗（暫定）
    ///     - isOk     : 成功時 1 / 失敗時 0
    ///     - objectCount: 推定された個数
    /// </summary>
    static AivbCountResult CountBgr24(
        const unsigned char* bgr,
        int width,
        int height,
        int strideBytes,
        AivbRoiRect roi,
        int minArea,
        int maxArea);

    /// <summary>
    /// 既定の面積フィルタ（暫定）。
    /// 実装チューニング後に値を見直す前提。
    /// </summary>
    static const int DefaultMinArea = 50;
    static const int DefaultMaxArea = 5000;
};
