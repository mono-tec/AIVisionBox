using System;
using System.Collections.Generic;
using System.Text;

namespace AIVisionBox.Core.Tests.Models
{
    public class ImageDataTests
    {
        [Fact]
        public void Ctor_ShouldStoreValues()
        {
            var buf = new byte[] { 0, 1, 2, 3 };

            var img = new ImageData(2, 2, ImageFormat.Gray8, buf);

            Assert.Equal(2, img.Width);
            Assert.Equal(2, img.Height);
            Assert.Equal(ImageFormat.Gray8, img.Format);

            // v0.x：コピーしない設計（性能/単純さ優先）
            Assert.Same(buf, img.Buffer);
        }
    }
}
