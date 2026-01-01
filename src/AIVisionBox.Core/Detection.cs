namespace AIVisionBox.Core
{
    /// <summary>
    /// AI 推論や画像処理によって検出された物体情報。
    /// 
    /// 主に Renesas RZ/V の Object Detection 系アプリ
    /// （例：Q08_object_counter）との連携を想定。
    /// </summary>
    public sealed class Detection
    {
        /// <summary>
        /// 検出クラス名（例："screw", "bolt"）
        /// </summary>
        public string Label { get; } = "";

        /// <summary>
        /// 検出信頼度（0.0 ～ 1.0）
        /// </summary>
        public float Score { get; }

        /// <summary>
        /// バウンディングボックス左上 X 座標（ピクセル）
        /// </summary>
        public int X { get; }

        /// <summary>
        /// バウンディングボックス左上 Y 座標（ピクセル）
        /// </summary>
        public int Y { get;  }

        /// <summary>
        /// バウンディングボックス幅（ピクセル）
        /// </summary>
        public int W { get;  }

        /// <summary>
        /// バウンディングボックス高さ（ピクセル）
        /// </summary>
        public int H { get;  }
        /// <summary>
        /// Detection を生成します。
        /// 
        /// - 本クラスは v0.x では「単純なデータ保持」を目的とし、入力値の厳密なバリデーションは行いません。
        /// - 値の妥当性（例：Score範囲、W/Hの正値、画像境界内か等）は、推論エンジン側または上位層で担保します。
        /// </summary>
        /// <param name="label">検出クラス名</param>
        /// <param name="score">検出信頼度（0.0～1.0想定）</param>
        /// <param name="x">左上X座標（px）</param>
        /// <param name="y">左上Y座標（px）</param>
        /// <param name="w">幅（px）</param>
        /// <param name="h">高さ（px）</param>
        public Detection(string label, float score, int x, int y, int w, int h)
        {
            Label = label ?? "";
            Score = score;
            X = x; Y = y; W = w; H = h;
        }

        public  string Test()
        {
            return $"Detection(Label={Label}, Score={Score:F2}, X={X}, Y={Y}, W={W}, H={H})";
        }
    }

}
