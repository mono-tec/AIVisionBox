namespace AIVisionBox.Engine.HttpClient.Dtos
{
    internal sealed class DetectionDto
    {
        public string label { get; set; } = "";
        public float score { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }
}
