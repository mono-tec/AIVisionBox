using System;
using System.Collections.Generic;
using System.Text;

namespace AIVisionBox.Core.Tests.Models
{
    public class DetectionTests
    {
        [Fact]
        public void Ctor_ShouldStoreValues()
        {
            var d = new Detection("screw", 0.9f, 10, 20, 30, 40);

            Assert.Equal("screw", d.Label);
            Assert.Equal(0.9f, d.Score);
            Assert.Equal(10, d.X);
            Assert.Equal(20, d.Y);
            Assert.Equal(30, d.W);
            Assert.Equal(40, d.H);
        }

        [Fact]
        public void Ctor_NullLabel_ShouldBecomeEmptyString()
        {
            var d = new Detection(null!, 0.5f, 0, 0, 1, 1);
            Assert.Equal("", d.Label);
        }
    }
}
