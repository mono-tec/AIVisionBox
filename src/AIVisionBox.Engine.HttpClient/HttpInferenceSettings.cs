using System;
using System.Collections.Generic;
using System.Text;

namespace AIVisionBox.Engine.HttpClient
{
    /// <summary>
    /// HTTP 推論サーバへ接続するための設定。
    /// 
    /// v0.x は「静止画1枚」を前提に、Base64で送る最小構成。
    /// </summary>
    public sealed class HttpInferenceSettings
    {
        /// <summary>例: "http://127.0.0.1:8080"</summary>
        public string BaseUrl { get; set; } = "http://127.0.0.1:8080";

        /// <summary>例: "/infer"</summary>
        public string Endpoint { get; set; } = "/infer";

        /// <summary>HTTPタイムアウト（ms）</summary>
        public int TimeoutMs { get; set; } = 3000;

        /// <summary>送信する画像フォーマット（例: "GRAY8" / "BGR24"）</summary>
        public string ImageFormat { get; set; } = "GRAY8";

        /// <summary>利用するモデル名（例: "Q08_object_counter"）。未使用でもOK。</summary>
        public string Model { get; set; } = "";

        /// <summary>スコア閾値など任意のオプション。サーバ側仕様に合わせる。</summary>
        public float ScoreThreshold { get; set; } = 0.3f;
    }
}
