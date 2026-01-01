using System;
using System.Collections.Generic;
using System.Text;

namespace AIVisionBox.Engine.HttpClient.Dtos
{
    internal sealed class InferResponseDto
    {
        public bool isOk { get; set; }
        public int objectCount { get; set; }
        public string message { get; set; } = "";
        public string engine { get; set; } = "";
        public string model { get; set; } = "";
        public List<DetectionDto> detections { get; set; } = new List<DetectionDto>();
    }
}
