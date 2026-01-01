using System;

namespace AIVisionBox.Core
{
    /// <summary>
    /// 推論エンジンへ渡す画像データの共通表現。
    /// 
    /// UI・カメラSDK・ファイル入力など、
    /// 入力元の違いをこのクラスで吸収する。
    /// </summary>
    public sealed class ImageData
    {
        /// <summary>
        /// 画像の横幅（ピクセル数）
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// 画像の高さ（ピクセル数）
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// 画像のピクセルフォーマット
        /// </summary>
        public ImageFormat Format { get; }

        /// <summary>
        /// 画像バッファ。
        /// 
        /// Gray8 の場合：
        ///   長さ = Width × Height
        ///   上から下、左から右の行優先（row-major）
        /// </summary>
        public byte[] Buffer { get; }

        /// <summary>
        /// ImageData を生成する。
        /// 
        /// ※ 本クラスではデータ妥当性（サイズ一致等）は厳密にチェックしない。
        ///    チェックは呼び出し側または Engine 側責務とする。
        /// </summary>
        public ImageData(int width, int height, ImageFormat format, byte[] buffer)
        {
            Width = width;
            Height = height;
            Format = format;
            Buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
        }
    }

}
