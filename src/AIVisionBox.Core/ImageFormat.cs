namespace AIVisionBox.Core
{
    /// <summary>
    /// 画像データのピクセルフォーマット定義。
    /// 
    /// v0.x では Gray8（8bit グレースケール）のみを正式サポートとする。
    /// Bgr24 等は将来拡張用。
    /// </summary>
    public enum ImageFormat
    {
        /// <summary>
        /// 8bit グレースケール（1ピクセル = 1byte）
        /// OpenCV / Renesas SDK との相互運用を最優先。
        /// </summary>
        Gray8,

        /// <summary>
        /// 8bit × 3ch（BGR）
        /// 将来、カラー入力が必要になった場合の拡張用。
        /// </summary>
        Bgr24
    }
}
