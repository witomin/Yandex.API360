using System;

namespace Yandex.API360 {
    public class Api360Options {
        /// <summary>
        /// ID организации
        /// </summary>
        private string _organizationId { get; }
        /// <summary>
        /// Токен авторизации
        /// </summary>
        internal string Token { get; }
        /// <summary>
        /// Базовый URL для обращения к API
        /// </summary>
        private string BaseUrl { get; } = "https://api360.yandex.net";
        /// <summary>
        /// Опции клиента API
        /// </summary>
        /// <param name="organizationId">Идентификатор организации</param>
        /// <param name="token">Токен для доступа к API</param>
        /// <param name="baseUrl">Базовый URL для обращения к API</param>
        /// <param name="maxResponseCount">Максимальное количество сущностей, которое может запрашиватья у API за 1 раз</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Api360Options(string organizationId, string token, string? baseUrl = default, int? maxResponseCount = default) {
            _organizationId = string.IsNullOrEmpty(organizationId) ? throw new ArgumentNullException(nameof(organizationId)) : organizationId;
            Token = string.IsNullOrEmpty(token) ? throw new ArgumentNullException(nameof(token)) : token;
            if (!string.IsNullOrEmpty(baseUrl)) {
                BaseUrl = baseUrl;
            }
            if (maxResponseCount.HasValue) {
                MaxResponseCount = (int)maxResponseCount;
            }
        }

        /// <summary>
        /// URL управления антиспамом
        /// </summary>
        internal string URLAntispam {
            get {
                return $"{BaseUrl}/admin/v1/org/{_organizationId}/mail/antispam/allowlist/ips";
            }
        }
        /// <summary>
        /// URL управления сотрудниками
        /// </summary>
        internal string URLUsers {
            get {
                return $"{BaseUrl}/directory/v1/org/{_organizationId}/users";
            }
        }
        /// <summary>
        /// URL управления сотрудниками
        /// </summary>
        internal string URLPostSettings {
            get {
                return $"{BaseUrl}/admin/v1/org/{_organizationId}/mail";
            }
        }
        /// <summary>
        /// URL управления подразделениями
        /// </summary>
        internal string URLDepartments {
            get {
                return $"{BaseUrl}/directory/v1/org/{_organizationId}/departments";
            }
        }
        /// <summary>
        /// URL управления группами
        /// </summary>
        internal string URLGroups {
            get {
                return $"{BaseUrl}/directory/v1/org/{_organizationId}/groups";
            }
        }
        /// <summary>
        /// URL 2FA
        /// </summary>
        internal string URL2fa {
            get {
                return $"{BaseUrl}/security/v1/org/{_organizationId}/domain_2fa";
            }
        }
        /// <summary>
        /// URL управления организацией
        /// </summary>
        internal string URLOrg {
            get {
                return $"{BaseUrl}/directory/v1/org";
            }
        }
        /// <summary>
        /// Максимальное количество организаций на странице ответ от API
        /// </summary>
        internal int MaxCountOrgInResponse {
            get {
                return 100;
            }
        }

        /// <summary>
        /// Максимальное количество сущностей в ответе API за 1 раз.
        /// Как выяснилось 17.03.2023, API отдает максимум 1000 пользователей за 1 раз.
        /// В документации такой информации нет, выяснено опытным путем.
        /// </summary>
        internal int MaxResponseCount { get; } = 1000;

        /// <summary>
        /// URL управления обработкой писем
        /// </summary>
        internal string URLrouting {
            get {
                return $"{BaseUrl}/admin/v1/org/{_organizationId}/mail/routing/rules";
            }
        }
        /// <summary>
        /// URL управления доменами
        /// </summary>
        internal string URLdomains {
            get {
                return $"{BaseUrl}/directory/v1/org/{_organizationId}/domains";
            }
        }
        /// <summary>
        /// URL управления параметрами паролей
        /// </summary>
        internal string URLpasswords {
            get {
                return $"{BaseUrl}/security/v1/org/{_organizationId}/domain_passwords";
            }
        }
        /// <summary>
        /// URL настроек безопасности
        /// </summary>
        internal string URLsecurity {
            get {
                return $"{BaseUrl}/security/v1/org/{_organizationId}";
            }
        }
        [Obsolete("Не поддерживается API Яндекс360 с 1 ноября 2024 г."/*, true*/)]
        /// <summary>
        /// URL управления доступом к почтовым ящикам
        /// </summary>
        internal string URLMailboxDelegation {
            get {
                return $"{BaseUrl}/admin/v1/org/{_organizationId}/mail/delegated";
            }
        }
        /// <summary>
        /// URL управления общими и делегированными почтовыми ящиками
        /// </summary>
        internal string URLMailboxManagement {
            get {
                return $"{BaseUrl}/admin/v1/org/{_organizationId}/mailboxes";
            }
        }
    }
}
