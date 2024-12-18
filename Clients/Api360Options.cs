﻿using System;

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
        /// URL управления антиспамом
        /// </summary>
        public string URLAntispam {
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
        /// URL управления сотрудниками
        /// </summary>
        public string URLPostSettings {
            get {
                return $"{API360Host}/admin/v1/org/{_organizationId}/mail";
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
        /// <summary>
        /// URL управления группами
        /// </summary>
        public string URLGroups {
            get {
                return $"{API360Host}/directory/v1/org/{_organizationId}/groups";
            }
        }
        /// <summary>
        /// URL 2FA
        /// </summary>
        public string URL2fa {
            get {
                return $"{API360Host}/security/v1/org/{_organizationId}/domain_2fa";
            }
        }
        /// <summary>
        /// URL управления организацией
        /// </summary>
        public string URLOrg {
            get {
                return $"{API360Host}/directory/v1/org";
            }
        }
        /// <summary>
        /// Максимальное количество организаций на странице ответ от API
        /// </summary>
        public int MaxCountOrgInResponse {
            get {
                return 100;
            }
        }

        /// <summary>
        /// URL управления обработкой писем
        /// </summary>
        public string URLrouting {
            get {
                return $"{API360Host}/admin/v1/org/{_organizationId}/mail/routing/rules";
            }
        }
        /// <summary>
        /// URL управления доменами
        /// </summary>
        public string URLdomains {
            get {
                return $"{API360Host}/directory/v1/org/{_organizationId}/domains";
            }
        }
        /// <summary>
        /// URL управления параметрами паролей
        /// </summary>
        public string URLpasswords {
            get {
                return $"{API360Host}/security/v1/org/{_organizationId}/domain_passwords";
            }
        }
        /// <summary>
        /// URL настроек безопасности
        /// </summary>
        public string URLsecurity {
            get {
                return $"{API360Host}/security/v1/org/{_organizationId}";
            }
        }
        [Obsolete("Не поддерживается API Яндекс360 с 1 ноября 2024 г."/*, true*/)]
        /// <summary>
        /// URL управления доступом к почтовым ящикам
        /// </summary>
        public string URLMailboxDelegation {
            get {
                return $"{API360Host}/admin/v1/org/{_organizationId}/mail/delegated";
            }
        }
        /// <summary>
        /// URL управления общими и делегированными почтовыми ящиками
        /// </summary>
        public string URLMailboxManagement {
            get {
                return $"{API360Host}/admin/v1/org/{_organizationId}/mailboxes";
            }
        }
    }
}
