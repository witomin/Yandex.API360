using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Yandex.API360.Exceptions;

namespace Yandex.API360 {
    public abstract class APIClient {
        internal Api360Options _options;
        internal HttpClient httpClient;

        internal JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        protected APIClient(Api360Options options) {
            _options = options;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", $"{Assembly.GetExecutingAssembly()?.GetName()?.Name}/{Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString()}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", _options.Token);
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
                    failedResponse = await response.Content.ReadFromJsonAsync<FailedAPIResponse>();
                    if (failedResponse != null) {
                        throw new APIRequestException(response.StatusCode, failedResponse);
                    }
                    else {
                        throw new APIRequestException(response.ReasonPhrase, response.StatusCode);
                    }
                }
                throw new APIRequestException(response.ReasonPhrase, response.StatusCode);
            }
        }

        internal async Task<TEntity> Get<TEntity>(string requestUri) {
            var response = await httpClient.GetAsync(requestUri);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }
        internal async Task<TEntity> Put<TEntity>(string requestUri, object value) {
            var response = await httpClient.PutAsJsonAsync(requestUri, value);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }
        internal async Task Delete(string requestUri) {
            var response = await httpClient.DeleteAsync(requestUri);
            await CheckResponseAsync(response);
        }
        internal async Task<TEntity> Delete<TEntity>(string requestUri) {
            var response = await httpClient.DeleteAsync(requestUri);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }
        internal async Task<TEntity> Post<TEntity>(string requestUri, object value) {
            var response = await httpClient.PostAsJsonAsync(requestUri, value);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }
        internal async Task<TEntity> Patch<TEntity>(string requestUri, object value, JsonSerializerOptions? options = default) {
            var response = await httpClient.PatchAsJsonAsync(requestUri, value, options);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }
    }
}
