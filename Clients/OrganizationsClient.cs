using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class OrganizationsClient :APIClient, IOrganizationClient{
        public OrganizationsClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<OrganizationList> GetListAsync(int? pageSize = 10, string? pageToken = null, CancellationToken cancellationToken = default) {
            return await Get<OrganizationList>($"{_options.URLOrg}?pageSize={pageSize}{(pageToken != null ? $"&pageToken={pageToken}" : string.Empty)}", cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Organization>> GetListAllAsync(CancellationToken cancellationToken = default) {
            var result = new List<Organization>();
            var response = await GetListAsync(_options.MaxCountOrgInResponse, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response.nextPageToken)) {
                result = response.organizations;
            }
            else {
                result.AddRange(response.organizations);
                do {
                    response = await GetListAsync(_options.MaxCountOrgInResponse, cancellationToken: cancellationToken).ConfigureAwait(false);
                    result.AddRange(response.organizations);
                }
                while (string.IsNullOrEmpty(response.nextPageToken));
            }
            return result;
        }
    }
}
