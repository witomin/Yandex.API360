using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class GroupsClient : APIClient, IGroupsClient {
        public GroupsClient(Api360Options options) : base(options) { }

        public async Task<GroupsList> GetListAsync(long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLGroups}?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<GroupsList>();
            return apiResponse;
        }

        public async Task<List<Group>> GetListAllAsync() {
            var response = await httpClient.GetAsync($"{_options.URLGroups}");
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<GroupsList>();
            var totalGroups = apiResponse.total;
            response = await httpClient.GetAsync($"{_options.URLGroups}?page={1}&perPage={totalGroups}");
            await CheckResponseAsync(response);
            apiResponse = await response.Content.ReadFromJsonAsync<GroupsList>();
            return apiResponse.groups;
        }

        public async Task<Group> AddAsync(BaseGroup group) {
            if (group is null) {
                throw new ArgumentNullException(nameof(group));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLGroups}", group);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }

        public async Task<bool> DeleteAsync(ulong groupId) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedElement>();
            return result.removed;
        }

        public async Task<bool> AddMemberAsync(long groupId, Member member) {
            if (member is null) {
                throw new ArgumentNullException(nameof(member));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLGroups}/{groupId}/members", member);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<AddedMember>();
            return result.added;
        }

        public async Task<bool> DeleteMemberAsync(ulong groupId, Member member) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}/members/{member.type}/{member.id}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DeletedMember>();
            return result.deleted;
        }

        public async Task<MembersList> DeleteAllMembersAsync(ulong groupId) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}/members");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<MembersList>();
            return result;
        }

        public async Task<Group> DeleteAllManagersAsync(ulong groupId) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}/admins");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<Group>();
            return result;
        }

        public async Task<MembersList> GetMembersAsync(ulong groupId) {
            var response = await httpClient.GetAsync($"{_options.URLGroups}/{groupId}/members");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<MembersList>();
        }

        public async Task<Group> GetByIdAsync(ulong groupId) {
            var response = await httpClient.GetAsync($"{_options.URLGroups}/{groupId}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }

        public async Task<Group> EditAsync(Group group) {
            var response = await httpClient.PatchAsJsonAsync<BaseGroup>($"{_options.URLGroups}/{group.id}", group);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }

        public async Task<Group> EditManagersAsync(ulong groupId, List<string> adminIds) {
            var response = await httpClient.PutAsJsonAsync($"{_options.URLGroups}/{groupId}/admins", new { adminIds = adminIds });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }

        public async Task<Group> EditMembersAsync(ulong groupId, List<Member> members) {
            var response = await httpClient.PutAsJsonAsync($"{_options.URLGroups}/{groupId}/members", new { members = members });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
    }
}
