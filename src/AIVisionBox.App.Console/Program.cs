using AIVisionBox.Core;
using AIVisionBox.Core.Interfaces;
using AIVisionBox.Engine.HttpClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AIVisionBox.App.ConsoleApp
{
    internal static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, config) =>
                {
                    // appsettings.json を読む（CreateDefaultBuilderでも読むが明示しておくと分かりやすい）
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((ctx, services) =>
                {
                    // 設定を読み込む
                    var settings = new HttpInferenceSettings();
                    ctx.Configuration.GetSection("Inference").Bind(settings);

                    // Settings を DI に登録（使い回し）
                    services.AddSingleton(settings);

                    // HttpClient を DI 管理で生成
                    services.AddHttpClient("InferenceClient", client =>
                    {
                        if (!string.IsNullOrWhiteSpace(settings.BaseUrl))
                            client.BaseAddress = new Uri(settings.BaseUrl);

                        if (settings.TimeoutMs > 0)
                            client.Timeout = TimeSpan.FromMilliseconds(settings.TimeoutMs);
                    });

                    // IAiInferenceEngine を HttpInferenceEngine にバインド
                    services.AddTransient<IAiInferenceEngine>(sp =>
                    {
                        var factory = sp.GetRequiredService<IHttpClientFactory>();
                        var http = factory.CreateClient("InferenceClient");

                        var engine = new HttpInferenceEngine(http);
                        engine.SetSettings(sp.GetRequiredService<HttpInferenceSettings>());
                        engine.Initialize("dummy"); // v0.x

                        return engine;
                    });

                    // 実行本体（HostedService）
                    services.AddHostedService<AppRunner>();
                })
                .Build();

            await host.RunAsync();
            return 0;
        }
    }

    internal sealed class AppRunner : IHostedService
    {
        private readonly IAiInferenceEngine _engine;

        public AppRunner(IAiInferenceEngine engine)
        {
            _engine = engine;
        }

        public Task StartAsync(System.Threading.CancellationToken cancellationToken)
        {
            System.Console.WriteLine("AIVisionBox ConsoleApp starting...");

            // v0.x: ダミー画像（1x1 Gray8）
            var img = new ImageData(
                width: 1,
                height: 1,
                format: ImageFormat.Gray8,
                buffer: new byte[] { 0x00 }
            );

            var r = _engine.Infer(img);

            System.Console.WriteLine($"IsOk        : {r.IsOk}");
            System.Console.WriteLine($"ObjectCount : {r.ObjectCount}");
            System.Console.WriteLine($"Engine      : {r.Engine}");
            System.Console.WriteLine($"Model       : {r.Model}");
            System.Console.WriteLine($"Message     : {r.Message}");
            System.Console.WriteLine($"Detections  : {r.Detections.Count}");

            foreach (var d in r.Detections)
            {
                System.Console.WriteLine($"  - {d.Label} score={d.Score} x={d.X} y={d.Y} w={d.W} h={d.H}");
            }

            System.Console.WriteLine("Done.");
            return Task.CompletedTask;
        }

        public Task StopAsync(System.Threading.CancellationToken cancellationToken)
        {
            // 何もしない（v0.x）
            return Task.CompletedTask;
        }
    }
}