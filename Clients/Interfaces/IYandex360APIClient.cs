namespace Yandex.API360 {
    public interface IYandex360APIClient {
        /// <summary>
        /// Операции связанные с общими ящиками
        /// </summary>
        IMailboxesClient Mailboxes { get; }
    }
}
