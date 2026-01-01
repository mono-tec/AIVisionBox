using AIVisionBox.Engine.HttpClient.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIVisionBox.Engine.HttpClient.Tests
{
    public class HttpInferenceEngine_SettingsTests
    {
        [Fact]
        public void SetSettings_ShouldApplyBaseAddressAndTimeout()
        {
            var handler = new FakeHttpMessageHandler(_ => FakeHttpMessageHandler.Json("{}"));
            var http = new System.Net.Http.HttpClient(handler);

            var engine = new HttpInferenceEngine(http);
            engine.SetSettings(new HttpInferenceSettings
            {
                BaseUrl = "http://localhost:5000",
                TimeoutMs = 1234,
                Endpoint = "/infer",
                Model = "test-model"
            });

            Assert.Equal(new Uri("http://localhost:5000"), http.BaseAddress);
            Assert.Equal(TimeSpan.FromMilliseconds(1234), http.Timeout);
        }
    }
}
