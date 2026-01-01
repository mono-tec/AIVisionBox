using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace AIVisionBox.Mock.InferenceServer.Tests
{
    public class InferServerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public InferServerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Root_ShouldReturnRunningMessage()
        {
            var client = _factory.CreateClient();

            var text = await client.GetStringAsync("/");

            Assert.Contains("Mock Inference Server is running", text);
        }

        [Fact]
        public async Task Infer_Post_ShouldReturnMockJson()
        {
            var client = _factory.CreateClient();

            var request = new
            {
                model = "MockModel",
                image = new { format = "GRAY8", width = 1, height = 1, dataBase64 = "AA==" }
            };

            var resp = await client.PostAsJsonAsync("/infer", request);
            resp.EnsureSuccessStatusCode();

            var json = await resp.Content.ReadAsStringAsync();

            Assert.Contains("\"isOk\": true", json);
            Assert.Contains("\"objectCount\": 5", json);
            Assert.Contains("\"engine\": \"MockServer\"", json);
            Assert.Contains("\"model\": \"MockModel\"", json);
            Assert.Contains("\"detections\"", json);
        }
        [Fact]
        public async Task Infer_Get_ShouldBeNotFound()
        {
            var client = _factory.CreateClient();
            var resp = await client.GetAsync("/infer");
            Assert.Equal(System.Net.HttpStatusCode.MethodNotAllowed, resp.StatusCode);
        }

    }
}
