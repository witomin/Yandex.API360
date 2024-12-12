using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class AuditClient: APIClient, IAuditClient {
        public AuditClient(Api360Options options) : base(options) { }
        
        public async Task<EventList> GetDiskLogAsync(uint pageSize, string pageToken = null, DateTime? beforeDate = null, DateTime? afterDate = null,
            List<string> includeUids = null, List<string> excludeUids = null) {

            var beforeDate_iso = beforeDate != null ? $"{ ((DateTime)beforeDate).ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture)}" : string.Empty;
            var afterDate_iso = afterDate != null ? $"{ ((DateTime)afterDate).ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture)}" : string.Empty;

            string combinedIncludeUids = string.Empty;
            string combinedExcludeUids = string.Empty;

            if (includeUids != null && includeUids.Count > 0) {
                var includeUids_str = includeUids.Select(x => $"includeUids={x}");
                combinedIncludeUids = string.Join("&", includeUids_str.ToArray());
            }
            if (excludeUids != null && excludeUids.Count > 0) {
                var excludeUids_str = excludeUids.Select(x => $"excludeUids={x}");
                combinedExcludeUids = string.Join("&", excludeUids_str.ToArray());
            }

            return await Get<EventList>($"{_options.URLsecurity}/audit_log/disk?pageSize={pageSize}{(!string.IsNullOrEmpty(pageToken) ? $"&pageToken={pageToken}" : string.Empty)}{(!string.IsNullOrEmpty(beforeDate_iso) ? $"&beforeDate={beforeDate_iso}" : string.Empty)}{(!string.IsNullOrEmpty(afterDate_iso) ? $"&afterDate={afterDate_iso}" : string.Empty)}{(!string.IsNullOrEmpty(combinedIncludeUids) ? $"&{combinedIncludeUids}" : string.Empty)}{(!string.IsNullOrEmpty(combinedExcludeUids) ? $"&{combinedExcludeUids}" : string.Empty)}");
        }
    }
}
