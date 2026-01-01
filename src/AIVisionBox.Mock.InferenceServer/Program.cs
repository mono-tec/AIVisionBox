//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// JSONを分かりやすく（デバッグ用）
builder.Services.ConfigureHttpJsonOptions(o =>
{
    o.SerializerOptions.WriteIndented = true;
});

var app = builder.Build();

app.MapGet("/", () => Results.Text("AIVisionBox Mock Inference Server is running."));

app.MapPost("/infer", async (HttpRequest request) =>
{
    // v0.x: 受信内容は厳密には見ない（将来ここでチェック）
    using var doc = await JsonDocument.ParseAsync(request.Body);

    // とりあえず固定の擬似結果を返す
    var response = new
    {
        isOk = true,
        objectCount = 5,
        message = "mock inference result",
        engine = "MockServer",
        model = doc.RootElement.TryGetProperty("model", out var m) ? m.GetString() : "",
        detections = new[]
        {
            new { label = "screw", score = 0.92, x = 10, y = 20, w = 30, h = 40 },
            new { label = "screw", score = 0.88, x = 60, y = 25, w = 28, h = 38 },
            new { label = "bolt",  score = 0.81, x = 15, y = 70, w = 35, h = 35 }
        }
    };

    return Results.Json(response);
});

app.Run();

public partial class Program { }

