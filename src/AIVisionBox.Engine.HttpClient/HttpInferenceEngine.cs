using AIVisionBox.Core;
using AIVisionBox.Core.Interfaces;
using AIVisionBox.Engine.HttpClient.Dtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace AIVisionBox.Engine.HttpClient
{
    /// <summary>
    /// HTTP 経由で外部推論サーバに静止画を投げ、結果を受け取る推論エンジン。
    /// 
    /// v0.x（スケルトン版）：
    /// - 実通信はまだ行わない（手元に推論BOXが無い前提）
    /// - 設定値と入出力契約だけ先に固める
    /// </summary>
    public sealed class HttpInferenceEngine : IAiInferenceEngine
    {
        private readonly System.Net.Http.HttpClient _http;
        private HttpInferenceSettings _settings = new HttpInferenceSettings();
        private bool _initialized;

        public HttpInferenceEngine()
        {
            _http = null!;
        }

        public HttpInferenceEngine(System.Net.Http.HttpClient httpClient)
        {
            _http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// ホスト側で設定を注入する（Console/Blazor側責務）。
        /// </summary>
        public void SetSettings(HttpInferenceSettings settings)
        {
            _settings = settings ?? new HttpInferenceSettings();

            if (!string.IsNullOrWhiteSpace(_settings.BaseUrl))
            {
                _http.BaseAddress = new Uri(_settings.BaseUrl, UriKind.Absolute);
            }

            if (_settings.TimeoutMs > 0)
            {
                _http.Timeout = TimeSpan.FromMilliseconds(_settings.TimeoutMs);
            }
        }

        /// <summary>
        /// 初期化。v0.xでは「設定がセットされたか」を確認する程度。
        /// </summary>
        public bool Initialize(string configPathOrName)
        {
            // configPathOrName は将来、設定ファイルパスやプロファイル名として使える
            _initialized = true;
            return true;
        }

        /// <summary>
        /// 1枚の画像を推論する。
        /// 
        /// v0.x（スケルトン）では HTTP通信は行わず、擬似結果を返す。
        /// 後でここに POST /infer を実装して置き換える。
        /// </summary>
        public InferenceResult Infer(ImageData img)
        {
            if (!_initialized)
                throw new InvalidOperationException("Engine is not initialized.");

            if (img == null)
                throw new ArgumentNullException(nameof(img));

            // ✅ v0.x: Gray8 のみ対応（契約テストに合わせる）
            if (img.Format != ImageFormat.Gray8)
            {
                return new InferenceResult
                {
                    IsOk = false,
                    ObjectCount = 0,
                    Message = "Unsupported image format: " + img.Format,
                    Engine = "HttpInference",
                    Model = _settings.Model ?? ""
                };
            }

            if (img.Buffer == null || img.Buffer.Length == 0)
            {
                return new InferenceResult
                {
                    IsOk = false,
                    ObjectCount = 0,
                    Message = "Invalid image buffer.",
                    Engine = "HttpInference",
                    Model = _settings.Model ?? ""
                };
            }


            var req = new InferRequestDto
            {
                model = _settings.Model ?? "",
                image = new ImageDto
                {
                    format = "GRAY8",
                    width = img.Width,
                    height = img.Height,
                    dataBase64 = Convert.ToBase64String(img.Buffer ?? Array.Empty<byte>())
                }
            };


            // 同期APIのまま進めるため、内部は GetAwaiter().GetResult() で待つ
            // （将来 InferAsync を追加してもよい）
            var endpoint = string.IsNullOrWhiteSpace(_settings.Endpoint) ? "/infer" : _settings.Endpoint;

            InferResponseDto? respDto;
            try
            {
                var resp = _http.PostAsJsonAsync(endpoint, req, CancellationToken.None)
                                .GetAwaiter().GetResult();

                resp.EnsureSuccessStatusCode();

                respDto = resp.Content
                              .ReadFromJsonAsync<InferResponseDto>(cancellationToken: CancellationToken.None)
                              .GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                return new InferenceResult
                {
                    IsOk = false,
                    ObjectCount = 0,
                    Message = "HTTP inference failed: " + ex.Message,
                    Engine = "HttpInference",
                    Model = _settings.Model ?? ""
                };
            }

            if (respDto == null)
            {
                return new InferenceResult
                {
                    IsOk = false,
                    ObjectCount = 0,
                    Message = "HTTP inference failed: empty response",
                    Engine = "HttpInference",
                    Model = _settings.Model ?? ""
                };
            }

            // DTO -> Core
            var result = new InferenceResult
            {
                IsOk = respDto.isOk,
                ObjectCount = respDto.objectCount,
                Message = respDto.message ?? "",
                Engine = string.IsNullOrWhiteSpace(respDto.engine) ? "HttpInference" : respDto.engine,
                Model = string.IsNullOrWhiteSpace(respDto.model) ? (_settings.Model ?? "") : respDto.model
            };

            if (respDto.detections != null)
            {
                foreach (var d in respDto.detections)
                {
                    // Detection のコンストラクタに合わせて引数順は調整してください
                    result.Detections.Add(new Detection(
                        d.label ?? "",
                        d.score,
                        d.x, d.y, d.w, d.h
                    ));
                }
            }

            return result;
        }

        public void Dispose()
        {
            // 将来 HttpClient を持つようになったら破棄対象をここに置く
        }


    }
}
