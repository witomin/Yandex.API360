using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    /// <summary>
    /// Endpoints для работы с аудитом
    /// </summary>
    public interface IAuditClient {
        /// <summary>
        /// Получить аудит лог Диска постранично
        /// </summary>
        /// <param name="pageSize">Количество событий на странице. Максимальное значение — 100</param>
        /// <param name="pageToken">Токен постраничной навигации</param>
        /// <param name="beforeDate">Верхняя граница периода выборки</param>
        /// <param name="afterDate">Нижняя граница периода выборки</param>
        /// <param name="includeUids">Список пользователей, действия которых должны быть включены в список событий</param>
        /// <param name="excludeUids">Список пользователей, действия которых должны быть исключены из списка событий</param>
        /// <returns></returns>
        public Task<EventList> GetDiskLogAsync(uint pageSize, string pageToken = null, DateTime? beforeDate = null, DateTime? afterDate = null,
            List<string> includeUids = null, List<string> excludeUids = null);
    }
}
