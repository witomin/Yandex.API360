namespace Yandex.API360 {
    public interface IYandex360APIClient {
        /// <summary>
        /// Операции с общими ящиками
        /// </summary>
        IMailboxesClient Mailboxes { get; }
        /// <summary>
        /// Операции с 2FA
        /// </summary>
        I2FAClient TwoFA { get; }
        /// <summary>
        /// Операции с настройками антиспама
        /// </summary>
        IAntispamClient Antispam { get; }
        /// <summary>
        /// Операции с аудитом
        /// </summary>
        IAuditClient Audit { get; }
        /// <summary>
        /// Операции с настройками аутентификации
        /// </summary>
        IAuthSettingsClient AuthSettings { get; }
        /// <summary>
        /// Операции с подразделениями
        /// </summary>
        IDepartmentsClient Departments { get; }
        /// <summary>
        /// Операции с DNS
        /// </summary>
        IDNSClient DNS { get; }
        /// <summary>
        /// Операции с доменами
        /// </summary>
        IDomainsClient Domains { get; }
        /// <summary>
        /// Операции с группами
        /// </summary>
        IGroupsClient Groups { get; }
        /// <summary>
        /// Операции с организациями
        /// </summary>
        IOrganizationClient Organization { get; }
        /// <summary>
        /// Управление паролями
        /// </summary>
        IPasswordManagementClient PasswordManagement { get; }
        /// <summary>
        /// Управление настройками почты сотрудников
        /// </summary>
        IPostSettingsClient PostSettings { get; }
        /// <summary>
        /// Управление правилами обработки писем
        /// </summary>
        IRoutingClient Routing { get; }
        /// <summary>
        /// Управление пользователями
        /// </summary>
        IUsersClient Users { get; }
    }
}
