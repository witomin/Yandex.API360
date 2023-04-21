using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public partial class Client {
        /// <summary>
        /// Получить список групп постранично
        /// </summary>
        /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество групп на одной странице ответа</param>
        /// <returns></returns>
        public async Task<GroupsList> GetGroupsAsync(long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLGroups}?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<GroupsList>();
            return apiResponse;
        }
        /// <summary>
        /// Получить полный списко групп
        /// </summary>
        /// <returns></returns>
        public async Task<List<Group>> GetAllGroupsAsync() {
            var response = await httpClient.GetAsync($"{_options.URLGroups}");
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<GroupsList>();
            var totalGroups = apiResponse.total;
            response = await httpClient.GetAsync($"{_options.URLGroups}?page={1}&perPage={totalGroups}");
            await CheckResponseAsync(response);
            apiResponse = await response.Content.ReadFromJsonAsync<GroupsList>();
            return apiResponse.groups;
        }
        /// <summary>
        /// Создать группу
        /// </summary>
        /// <param name="group">группа</param>
        /// <returns></returns>
        public async Task<Group> AddGroupAsync(BaseGroup group) {
            if (group is null) {
                throw new ArgumentNullException(nameof(group));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLGroups}", group);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
        /// <summary>
        /// Удалить группу
        /// </summary>
        /// <param name="groupId">идентификатор группы</param>
        /// <returns></returns>
        public async Task<bool> DeleteGroupAsync(ulong groupId) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedElement>();
            return result.removed;
        }
        /// <summary>
        /// Добавить участника в группу
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="member">Участник группы</param>
        /// <returns></returns>
        public async Task<bool> AddMemberToGroupAsync(long groupId, Member member) {
            if (member is null) {
                throw new ArgumentNullException(nameof(member));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLGroups}/{groupId}/members", member);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<AddedMember>();
            return result.added;
        }
        /// <summary>
        /// Удалить участника группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="member">Участник группы</param>
        /// <returns></returns>
        public async Task<bool> DeleteMemberFromGroupAsync(ulong groupId, Member member) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}/members/{member.type}/{member.id}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DeletedMember>();
            return result.deleted;
        }
        /// <summary>
        /// Удалить всех участнков группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <returns></returns>
        public async Task<MembersList> DeleteAllMembersFromGroupAsync(ulong groupId) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}/members");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<MembersList>();
            return result;
        }
        /// <summary>
        /// Удалить всех руководителей группы
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<Group> DeleteAllManagersFromGroupAsync(ulong groupId) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}/admins");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<Group>();
            return result;
        }
        /// <summary>
        /// Получить список участников группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <returns></returns>
        public async Task<MembersList> GetGroupMembersAsync(ulong groupId) {
            var response = await httpClient.GetAsync($"{_options.URLGroups}/{groupId}/members");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<MembersList>();
        }
        /// <summary>
        /// Получить группу
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <returns></returns>
        public async Task<Group> GetGroupAsync(long groupId) {
            var response = await httpClient.GetAsync($"{_options.URLGroups}/{groupId}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
        /// <summary>
        /// Изменить группу
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns></returns>
        public async Task<Group> EditGroupAsync(Group group) {
            var response = await httpClient.PatchAsJsonAsync<BaseGroup>($"{_options.URLGroups}/{group.id}", group);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
        /// <summary>
        /// Изменить руководителей группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="adminIds">Идентификаторы руководителей группы</param>
        /// <returns></returns>
        public async Task<Group> EditManagersFromGroupAsync(ulong groupId, List<string> adminIds) {
            var response = await httpClient.PutAsJsonAsync($"{_options.URLGroups}/{groupId}/admins", new { adminIds = adminIds });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
        /// <summary>
        /// Изменить список участников группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <param name="members">Участники группы</param>
        /// <returns></returns>
        public async Task<Group> EditMembersFromGroupAsync(ulong groupId, List<Member> members) {
            var response = await httpClient.PutAsJsonAsync($"{_options.URLGroups}/{groupId}/members", new { members = members });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
    }
}
