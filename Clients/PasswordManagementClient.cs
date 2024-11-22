using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class PasswordManagementClient :APIClient, IPasswordManagementClient {
        public PasswordManagementClient(Api360Options options) : base(options) { }

        public async Task<PasswordParameters> GetPasswordParametersAsync() {
            var response = await httpClient.GetAsync(_options.URLpasswords);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<PasswordParameters>();
            return result;
        }

        public async Task<PasswordParameters> EditPasswordParametersAsync(PasswordParameters parameters) {
            if (parameters is null) {
                throw new ArgumentNullException(nameof(parameters));
            }
            var response = await httpClient.PutAsJsonAsync(_options.URLpasswords, parameters);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<PasswordParameters>();
        }
    }
}
