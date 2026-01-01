using System;
using System.Collections.Generic;
using System.Text;

namespace AIVisionBox.Core.Tests.Models
{
    public class InferenceResultTests
    {
        [Fact]
        public void NewResult_ShouldHaveDetectionsList()
        {
            var r = new InferenceResult();

            Assert.NotNull(r.Detections);
            Assert.Empty(r.Detections);
        }

        [Fact]
        public void CanAddDetection()
        {
            var r = new InferenceResult();
            r.Detections.Add(new Detection("screw", 0.8f, 1, 2, 3, 4));

            Assert.Single(r.Detections);
            Assert.Equal("screw", r.Detections[0].Label);
        }
    }
}
