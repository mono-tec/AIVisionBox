namespace AIVisionBox.Engine.HttpClient.Dtos
{
    internal sealed class ImageDto
    {
        public string format { get; set; } = "GRAY8";
        public int width { get; set; }
        public int height { get; set; }
        public string dataBase64 { get; set; } = "";
    }
}
