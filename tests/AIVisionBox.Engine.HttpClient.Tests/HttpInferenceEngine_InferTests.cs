using AIVisionBox.Core;
using AIVisionBox.Engine.HttpClient.Tests.Utils;
using System.Net;
using System.Text;


namespace AIVisionBox.Engine.HttpClient.Tests
{
    public class HttpInferenceEngine_InferTests
    {
        [Fact]
        public void Infer_ShouldReturnParsedResult()
        {
            var handler = new FakeHttpMessageHandler(req =>
            {
                Assert.Equal("/infer", req.RequestUri!.AbsolutePath);
                Assert.Equal(HttpMethod.Post, req.Method);

                var json = """
            {
              "isOk": true,
              "objectCount": 5,
              "message": "mock inference result",
              "engine": "MockServer",
              "model": "MockModel",
              "detections": [
                { "label":"screw", "score":0.92, "x":10, "y":20, "w":30, "h":40 }
              ]
            }
            """;

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
            });

            var http = new System.Net.Http.HttpClient(handler)
            {
                BaseAddress = new System.Uri("http://localhost:5000")
            };

            var engine = new HttpInferenceEngine(http);
            engine.Initialize("dummy");
            engine.SetSettings(new HttpInferenceSettings
            {
                BaseUrl = "http://localhost:5000",
                Endpoint = "/infer",
                TimeoutMs = 3000,
                Model = "MockModel",
                ImageFormat = "GRAY8"
            });

            var img = new ImageData(1, 1, ImageFormat.Gray8, new byte[] { 0x00 });

            var result = engine.Infer(img);

            Assert.True(result.IsOk);
            Assert.Equal(5, result.ObjectCount);
            Assert.Equal("MockServer", result.Engine);
            Assert.Equal("MockModel", result.Model);
            Assert.Single(result.Detections);
        }
    }
}
