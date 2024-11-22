
namespace Yandex.API360 {
    public partial class Client : APIClient, IYandex360APIClient {
        public Client(Api360Options options) : base(options) {
            Mailboxes = new MailboxesClient(_options);
            TwoFA = new TwoFAClient(_options);
            Antispam = new AntispamClient(_options);
            Audit = new AuditClient(_options);
            AuthSettings = new AuthSettingsClient(_options);
            Departments = new DepartmentsClient(_options);
            DNS = new DNSClient(_options);
            Domains = new DomainsClient(_options);
            Groups = new GroupsClient(_options);
            Organization = new OrganizationsClient(_options);
            PasswordManagement =new PasswordManagementClient(_options);
            PostSettings = new PostSettingsClient(_options);
            Routing = new RoutingClient(_options);
            Users=new UsersClient(_options);
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
        public  IRoutingClient Routing { get; }
        public IUsersClient Users { get; }
    }
}




