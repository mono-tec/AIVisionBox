using System.Collections.Generic;

namespace AIVisionBox.Core
{
    /// <summary>
    /// 推論結果の共通表現。
    /// 
    /// ・OpenCV（ブロブ解析）
    /// ・Renesas SDK（AI推論）
    /// の両方で共通して扱える形を意図している。
    /// 
    /// PLC 連携・HMI 表示・ログ出力は本クラスを基準に行う。
    /// </summary>
    public sealed class InferenceResult
    {
        /// <summary>
        /// 現場向けの最終判定結果。
        /// 
        /// 例：
        /// - 個数が範囲内 → true
        /// - 検出失敗／通信失敗 → false
        /// 
        /// 判定ロジック自体は Engine または上位層で決める。
        /// </summary>
        public bool IsOk { get; set; }

        /// <summary>
        /// 検出された物体数。
        /// 
        /// ・OpenCV：輪郭／ブロブ数
        /// ・AI：Detection の件数、またはラベル別集計
        /// </summary>
        public int ObjectCount { get; set; }

        /// <summary>
        /// AI 推論時の詳細検出結果。
        /// 
        /// OpenCV のみ使用する場合は空のままで問題ない。
        /// </summary>
        public List<Detection> Detections { get; } = new List<Detection>();

        /// <summary>
        /// ログ・デバッグ用メッセージ。
        /// 例：エラー理由、判定根拠など。
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// 使用した推論エンジン名。
        /// 例："OpenCvBlob", "RenesasRzvHttp"
        /// </summary>
        public string Engine { get; set; } = "";

        /// <summary>
        /// 使用したモデル名（AI 使用時）。
        /// 例："Q08_object_counter"
        /// </summary>
        public string Model { get; set; } = "";
    }
}


