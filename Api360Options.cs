using System;

namespace Yandex.API360 {
    public class Api360Options {
        /// <summary>
        /// Базовый хост API
        /// </summary>
        const string BaseUrl = "https://api360.yandex.net";
        /// <summary>
        /// ID организации
        /// </summary>
        readonly string _organizationId;
        /// <summary>
        /// Токен авторизации
        /// </summary>
        public string Token { get; }
        readonly string API360Host;

        public Api360Options(string organizationId, string token, string? baseUrl = default) {
            _organizationId = string.IsNullOrEmpty(organizationId) ? throw new ArgumentNullException(nameof(organizationId)) : organizationId;
            Token = string.IsNullOrEmpty(token) ? throw new ArgumentNullException(nameof(token)) : token;
            API360Host = string.IsNullOrEmpty(baseUrl) ? BaseUrl : baseUrl;
        }
        
        /// <summary>
        /// URL управления белыми списками
        /// </summary>
        public string URLWhiteList {
            get {
                return $"{API360Host}/admin/v1/org/{_organizationId}/mail/antispam/allowlist/ips";
            }
        }
        /// <summary>
        /// URL управления сотрудниками
        /// </summary>
        public string URLUsers {
            get {
                return $"{API360Host}/directory/v1/org/{_organizationId}/users";
            }
        }
        /// <summary>
        /// URL управления подразделениями
        /// </summary>
        public string URLDepartments {
            get {
                return $"{API360Host}/directory/v1/org/{_organizationId}/departments";
            }
        }
    }
}
