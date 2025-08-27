
using Microsoft.Extensions.Logging;

namespace Yandex.API360 {
    public partial class Client : APIClient, IYandex360APIClient {
        public Client(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) {
            Mailboxes = new MailboxesClient(_options, _logger);
            TwoFA = new TwoFAClient(_options, _logger);
            Antispam = new AntispamClient(_options, _logger);
            Audit = new AuditClient(_options, _logger);
            AuthSettings = new AuthSettingsClient(_options, _logger);
            Departments = new DepartmentsClient(_options, _logger);
            DNS = new DNSClient(_options, _logger);
            Domains = new DomainsClient(_options, _logger);
            Groups = new GroupsClient(_options, _logger);
            Organization = new OrganizationsClient(_options, _logger);
            PasswordManagement = new PasswordManagementClient(_options, _logger);
            PostSettings = new PostSettingsClient(_options, _logger);
            Routing = new RoutingClient(_options, _logger);
            Users = new UsersClient(_options, _logger);
        }

        public IMailboxesClient Mailboxes { get; }
        public I2FAClient TwoFA { get; }
        public IAntispamClient Antispam { get; }
        public IAuditClient Audit { get; }
        public IAuthSettingsClient AuthSettings { get; }
        public IDepartmentsClient Departments { get; }
        public IDNSClient DNS { get; }
        public IDomainsClient Domains { get; }
        public IGroupsClient Groups { get; }
        public IOrganizationClient Organization { get; }
        public IPasswordManagementClient PasswordManagement { get; }
        public IPostSettingsClient PostSettings { get; }
        public IRoutingClient Routing { get; }
        public IUsersClient Users { get; }
    }
}




