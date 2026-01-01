using AIVisionBox.Core;
using AIVisionBox.Core.Interfaces;

namespace AIVisionBox.App.Console
{
    internal sealed class FakeEngine : IAiInferenceEngine
    {
        public bool Initialize(string configPathOrName) => true;

        public InferenceResult Infer(ImageData image)
        {
            return new InferenceResult
            {
                Engine = "Fake",
                Model = "N/A",
                IsOk = true,
                ObjectCount = 5,
                Message = "fake inference"
            };
        }

        public void Dispose() { }
    }
}
