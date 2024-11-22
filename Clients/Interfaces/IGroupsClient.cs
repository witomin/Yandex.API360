using System.Collections.Generic;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    /// <summary>
    /// Endpoints для управления группами
    /// </summary>
    public  interface IGroupsClient {
        /// <summary>
        /// Получить список групп постранично
        /// </summary>
        /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество групп на одной странице ответа</param>
        /// <returns></returns>
        public Task<GroupsList> GetListAsync(long page = 1, long perPage = 10);
        /// <summary>
        /// Получить полный списко групп
        /// </summary>
        /// <returns></returns>
        public Task<List<Group>> GetListAllAsync();
        /// <summary>
        /// Создать группу
        /// </summary>
        /// <param name="group">группа</param>
        /// <returns></returns>
        public Task<Group> AddAsync(BaseGroup group);
        /// <summary>
        /// Удалить группу
        /// </summary>
        /// <param name="groupId">идентификатор группы</param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(ulong groupId);
        /// <summary>
        /// Добавить участника в группу
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="member">Участник группы</param>
        /// <returns></returns>
        public Task<bool> AddMemberAsync(long groupId, Member member);
        /// <summary>
        /// Удалить участника группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="member">Участник группы</param>
        /// <returns></returns>
        public Task<bool> DeleteMemberAsync(ulong groupId, Member member);
        /// <summary>
        /// Удалить всех участнков группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <returns></returns>
        public Task<MembersList> DeleteAllMembersAsync(ulong groupId);
        /// <summary>
        /// Удалить всех руководителей группы
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Task<Group> DeleteAllManagersAsync(ulong groupId);
        /// <summary>
        /// Получить список участников группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <returns></returns>
        public Task<MembersList> GetMembersAsync(ulong groupId);
        /// <summary>
        /// Получить группу
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <returns></returns>
        public Task<Group> GetByIdAsync(ulong groupId);
        /// <summary>
        /// Изменить группу
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns></returns>
        public Task<Group> EditAsync(Group group);
        /// <summary>
        /// Изменить руководителей группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="adminIds">Идентификаторы руководителей группы</param>
        /// <returns></returns>
        public Task<Group> EditManagersAsync(ulong groupId, List<string> adminIds);
        /// <summary>
        /// Изменить список участников группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <param name="members">Участники группы</param>
        /// <returns></returns>
        public Task<Group> EditMembersAsync(ulong groupId, List<Member> members);
    }
}
