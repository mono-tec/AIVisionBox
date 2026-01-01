using AIVisionBox.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIVisionBox.Core.Tests.Fakes
{
    internal sealed class FakeEngine : IAiInferenceEngine
    {
        public bool Initialized { get; private set; }

        public bool Initialize(string configPathOrName)
        {
            Initialized = true;
            return true;
        }

        public InferenceResult Infer(ImageData image)
        {
            return new InferenceResult
            {
                Engine = "Fake",
                Model = "N/A",
                IsOk = true,
                ObjectCount = 1,
                Message = "ok"
            };
        }

        public void Dispose() { }
    }
}
