namespace AIVisionBox.Engine.HttpClient.Dtos
{
    internal sealed class InferRequestDto
    {
        public string model { get; set; } = "";
        public ImageDto image { get; set; } = new ImageDto();
    }
}
