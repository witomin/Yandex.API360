using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class PasswordManagementClient :APIClient, IPasswordManagementClient {
        public PasswordManagementClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<PasswordParameters> GetParametersAsync() {            
            return await Get<PasswordParameters>(_options.URLpasswords);
        }

        public async Task<PasswordParameters> SetParametersAsync(PasswordParameters parameters) {
            if (parameters is null) {
                throw new ArgumentNullException(nameof(parameters));
            }
            return await Put<PasswordParameters>(_options.URLpasswords, parameters);
        }
    }
}
