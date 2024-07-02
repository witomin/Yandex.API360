using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Yandex.API360.Exceptions;
using System.Reflection;


namespace Yandex.API360 {
    public partial class Client {
        Api360Options _options;
        HttpClient httpClient;

        HttpClient httpClientImg;

        JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public Client(Api360Options options) {
            _options = options;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", $"{Assembly.GetExecutingAssembly()?.GetName()?.Name}/{Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString()}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.Token);

            httpClientImg = new HttpClient();
            httpClientImg.DefaultRequestHeaders.Add("User-Agent", $"{Assembly.GetExecutingAssembly()?.GetName()?.Name}/{Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString()}");
            httpClientImg.DefaultRequestHeaders.Accept.Clear();
            //httpClientImg.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/png"));
            httpClientImg.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.Token);
        }

        /// <summary>
        /// Проверить ответ API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task CheckResponseAsync(HttpResponseMessage response) {
            var Codes = new List<System.Net.HttpStatusCode> {
            System.Net.HttpStatusCode.Unauthorized,
            System.Net.HttpStatusCode.Forbidden,
            System.Net.HttpStatusCode.NotFound,
            System.Net.HttpStatusCode.InternalServerError,
            System.Net.HttpStatusCode.BadRequest
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
    }
}




