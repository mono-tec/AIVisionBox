using AIVisionBox.Core.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIVisionBox.Core.Tests
{
    public class EngineContractTests
    {
        [Fact]
        public void Engine_ShouldInitializeAndInfer()
        {
            using var eng = new FakeEngine();

            Assert.True(eng.Initialize("dummy"));

            var img = new ImageData(1, 1, ImageFormat.Gray8, new byte[] { 0 });
            var r = eng.Infer(img);

            Assert.Equal("Fake", r.Engine);
            Assert.True(r.IsOk);
            Assert.Equal(1, r.ObjectCount);
        }
    }
}
