using System;
using System.Collections.Generic;
using System.Text;

namespace AIVisionBox.Engine.HttpClient
{
    /// <summary>
    /// 推論サーバへ送るリクエスト（暫定仕様）。
    /// まずはこの形で固定し、実機（RZ/V）側が整ったら合わせる。
    /// </summary>
    public sealed class InferRequest
    {
        public string Model { get; set; } = "";
        public ImagePayload Image { get; set; } = new ImagePayload();
        public Dictionary<string, object>? Options { get; set; }
    }

    public sealed class ImagePayload
    {
        public string Format { get; set; } = "GRAY8";
        public int Width { get; set; }
        public int Height { get; set; }
        public string DataBase64 { get; set; } = "";
    }

    /// <summary>
    /// 推論サーバから返ってくるレスポンス（暫定仕様）。
    /// </summary>
    public sealed class InferResponse
    {
        public bool IsOk { get; set; }
        public int ObjectCount { get; set; }
        public string Message { get; set; } = "";
        public string? Engine { get; set; }
        public string? Model { get; set; }
        public List<DetectionDto>? Detections { get; set; }
    }

    public sealed class DetectionDto
    {
        public string Label { get; set; } = "";
        public float Score { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
    }

}
