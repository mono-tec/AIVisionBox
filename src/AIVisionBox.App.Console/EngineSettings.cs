using System;
using System.Collections.Generic;
using System.Text;

namespace AIVisionBox.App.ConsoleApp
{
    public sealed class EngineSettings
    {
        public string Type { get; set; } = "Fake";
    }

    public sealed class AppSettings
    {
        public EngineSettings Engine { get; set; } = new EngineSettings();
    }
}
