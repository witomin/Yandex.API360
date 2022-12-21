using System;

namespace Yandex.API360.Models {
    /// <summary>
    /// Статус 2FA
    /// </summary>
    public class DomainStatus2FA {
        /// <summary>
        /// Период (в секундах), в течение которого при включенной 2FA пользователю в процессе авторизации предлагается настроить 2FA с возможностью пропустить этот шаг. По истечении периода возможность отложить настройку 2FA отключается.
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// Статус обязательной 2FA: true — включена; false — выключена.
        /// </summary>
        public bool enabled { get; set; }
        /// <summary>
        /// Время включения 2FA.
        /// </summary>
        public DateTime? enabledAt { get; set; }
    }
}
