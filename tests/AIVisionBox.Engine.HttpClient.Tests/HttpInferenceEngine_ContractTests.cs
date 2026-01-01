using AIVisionBox.Core;
using AIVisionBox.Engine.HttpClient.Tests.Utils;

namespace AIVisionBox.Engine.HttpClient.Tests
{
    public class HttpInferenceEngine_ContractTests
    {
        [Fact]
        public void Infer_WithoutInitialize_ShouldThrow()
        {
            var handler = new FakeHttpMessageHandler(_ => FakeHttpMessageHandler.Json("{}"));
            var http = new System.Net.Http.HttpClient(handler);

            var engine = new HttpInferenceEngine(http);
            engine.SetSettings(new HttpInferenceSettings());

            var img = new ImageData(1, 1, ImageFormat.Gray8, new byte[] { 0 });

            Assert.Throws<System.InvalidOperationException>(() => engine.Infer(img));
        }

        [Fact]
        public void Infer_UnsupportedFormat_ShouldReturnIsOkFalse()
        {
            var handler = new FakeHttpMessageHandler(_ => FakeHttpMessageHandler.Json("{}"));
            var http = new System.Net.Http.HttpClient(handler);

            var engine = new HttpInferenceEngine(http);
            engine.SetSettings(new HttpInferenceSettings());
            engine.Initialize("dummy");

            var img = new ImageData(1, 1, ImageFormat.Bgr24, new byte[] { 0, 0, 0 });

            var r = engine.Infer(img);

            Assert.False(r.IsOk);
            Assert.Contains("Unsupported", r.Message);
        }
    }
}
