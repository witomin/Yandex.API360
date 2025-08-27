using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Exceptions;

namespace Yandex.API360 {
    public abstract class APIClient {
        protected readonly Api360Options _options;
        protected readonly HttpClient _httpClient;
        protected readonly ILogger<APIClient>? _logger;

        private readonly JsonSerializerOptions _logSerializeOptions = new JsonSerializerOptions {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = false
        };

        protected APIClient(Api360Options options, ILogger<APIClient>? logger = default) {
            _logger = logger;
            _options = options;
            _httpClient = new HttpClient();
            ConfigureHttpClient();
        }

        private void ConfigureHttpClient() {
            var assemblyName = Assembly.GetExecutingAssembly()?.GetName();
            var userAgent = $"{assemblyName?.Name}/{assemblyName?.Version}";

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", _options.Token);
        }

        /// <summary>
        /// Проверить ответ API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal async Task CheckResponseAsync(HttpResponseMessage response) {
            var Codes = new List<System.Net.HttpStatusCode> {
            System.Net.HttpStatusCode.Unauthorized,
            System.Net.HttpStatusCode.Forbidden,
            System.Net.HttpStatusCode.NotFound,
            System.Net.HttpStatusCode.InternalServerError,
            System.Net.HttpStatusCode.BadRequest,
            System.Net.HttpStatusCode.Conflict
            };
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                if (response.Content is null) {
                    throw new APIRequestException("Response doesn't contain any content", response.StatusCode);
                }
                if (Codes.Contains(response.StatusCode)) {
                    FailedAPIResponse failedResponse;
                    failedResponse = await response.Content.ReadFromJsonAsync<FailedAPIResponse>().ConfigureAwait(false);
                    if (failedResponse != null) {
                        throw new APIRequestException(response.StatusCode, failedResponse);
                    }
                    else {
                        throw new APIRequestException(response.ReasonPhrase ?? "Unknown error", response.StatusCode);
                    }
                }
                throw new APIRequestException(response.ReasonPhrase ?? "Unknown error", response.StatusCode);
            }
        }

        private void LogRequest(HttpResponseMessage response, object? body) {
            _logger?.LogInformation("API Request: {RequestDetails}",
                JsonSerializer.Serialize(new {
                    response.RequestMessage?.RequestUri,
                    response.RequestMessage?.Method,
                    Headers = response.RequestMessage?.Headers.ToDictionary(h => h.Key, h => h.Value),
                    Body = body
                }, _logSerializeOptions));
        }

        internal async Task<TEntity> Get<TEntity>(string requestUri, CancellationToken cancellationToken = default) {
            var response = await _httpClient.GetAsync(requestUri, cancellationToken);
            LogRequest(response, null);
            await CheckResponseAsync(response).ConfigureAwait(false);
            return await response.Content.ReadFromJsonAsync<TEntity>(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        internal async Task<TEntity> Put<TEntity>(string requestUri, object body, JsonSerializerOptions? options = default,
            CancellationToken cancellationToken = default) {
            var response = await _httpClient.PutAsJsonAsync(requestUri, body, options, cancellationToken).ConfigureAwait(false);
            LogRequest(response, body);
            await CheckResponseAsync(response).ConfigureAwait(false);
            return await response.Content.ReadFromJsonAsync<TEntity>(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        internal async Task Delete(string requestUri, CancellationToken cancellationToken = default) {
            var response = await _httpClient.DeleteAsync(requestUri, cancellationToken).ConfigureAwait(false);
            LogRequest(response, null);
            await CheckResponseAsync(response).ConfigureAwait(false);
        }
        internal async Task<TEntity> Delete<TEntity>(string requestUri, CancellationToken cancellationToken = default) {
            var response = await _httpClient.DeleteAsync(requestUri, cancellationToken).ConfigureAwait(false);
            LogRequest(response, null);
            await CheckResponseAsync(response).ConfigureAwait(false);
            return await response.Content.ReadFromJsonAsync<TEntity>(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        internal async Task<TEntity> Post<TEntity>(string requestUri, object body, JsonSerializerOptions? options = default,
            CancellationToken cancellationToken = default) {
            var response = await _httpClient.PostAsJsonAsync(requestUri, body, options, cancellationToken).ConfigureAwait(false);
            LogRequest(response, body);
            await CheckResponseAsync(response).ConfigureAwait(false);
            return await response.Content.ReadFromJsonAsync<TEntity>(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        internal async Task<TEntity> Patch<TEntity>(string requestUri, object body, JsonSerializerOptions? options = default,
            CancellationToken cancellationToken = default) {
            var response = await _httpClient.PatchAsJsonAsync(requestUri, body, options, cancellationToken).ConfigureAwait(false);
            LogRequest(response, body);
            await CheckResponseAsync(response).ConfigureAwait(false);
            return await response.Content.ReadFromJsonAsync<TEntity>(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
