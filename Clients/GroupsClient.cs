using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class GroupsClient : APIClient, IGroupsClient {
        public GroupsClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<GroupsList> GetListAsync(long page = 1, long perPage = 10) {
            return await Get<GroupsList>($"{_options.URLGroups}?page={page}&perPage={perPage}");
        }

        public async Task<List<Group>> GetListAllAsync() {
            var result = new List<Group>();
            var response = await GetListAsync(1, _options.MaxResponseCount);
            result.AddRange(response.groups);
            var pages = response.pages;
            for (long i = 2; i <= pages; i++) {
                response = await GetListAsync(i, _options.MaxResponseCount);
                result.AddRange(response.groups);
            }
            return result;
        }

        public async Task<Group> AddAsync(BaseGroup group) {
            if (group is null) {
                throw new ArgumentNullException(nameof(group));
            }
            return await Post<Group>($"{_options.URLGroups}", group);
        }

        public async Task<bool> DeleteAsync(ulong groupId) {
            var result = await Delete<RemovedElement>($"{_options.URLGroups}/{groupId}");
            return result.removed;
        }

        public async Task<bool> AddMemberAsync(long groupId, Member member) {
            if (member is null) {
                throw new ArgumentNullException(nameof(member));
            }
            var result = await Post<AddedMember>($"{_options.URLGroups}/{groupId}/members", member);
            return result.added;
        }

        public async Task<bool> DeleteMemberAsync(ulong groupId, Member member) {
            var result = await Delete<DeletedMember>($"{_options.URLGroups}/{groupId}/members/{member.type}/{member.id}");
            return result.deleted;
        }

        public async Task<MembersList> DeleteAllMembersAsync(ulong groupId) {
            var result = await Delete<MembersList>($"{_options.URLGroups}/{groupId}/members");
            return result;
        }

        public async Task<Group> DeleteAllManagersAsync(ulong groupId) {
            var result = await Delete<Group>($"{_options.URLGroups}/{groupId}/admins");
            return result;
        }

        public async Task<MembersList> GetMembersAsync(ulong groupId) {
            return await Get<MembersList>($"{_options.URLGroups}/{groupId}/members");
        }

        public async Task<Group> GetByIdAsync(ulong groupId) {
            return await Get<Group>($"{_options.URLGroups}/{groupId}");
        }

        public async Task<Group> EditAsync(Group group) {
            return await Patch<Group>($"{_options.URLGroups}/{group.id}", group as BaseGroup);
        }

        public async Task<Group> EditManagersAsync(ulong groupId, List<string> adminIds) {
            return await Put<Group>($"{_options.URLGroups}/{groupId}/admins", new { adminIds });
        }

        public async Task<Group> EditMembersAsync(ulong groupId, List<Member> members) {
            return await Put<Group>($"{_options.URLGroups}/{groupId}/members", new { members });
        }

        public async Task<MembersList2> GetMembers2Async(ulong groupId) {
            return await Get<MembersList2>($"{_options.URLGroups2}/{groupId}/members");
        }

        public async Task<bool> AddMembersAsync(ulong groupId, List<ulong> departmentIds = null, List<ulong> userIds = null, List<ulong> groupIds = null, List<ulong> externalContactIds = null) {
            var resuls = await Patch<object>($"{_options.URLGroups2}/{groupId}/members/add",
                new { departmentIds, externalContactIds, groupIds, userIds },
                new System.Text.Json.JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            return resuls is { };
        }

        public async Task<bool> DeleteMembersAsync(long groupId, List<ulong> departmentIds = null, List<ulong> userIds = null, List<ulong> groupIds = null, List<ulong> externalContactIds = null) {
            var resuls = await Patch<object>($"{_options.URLGroups2}/{groupId}/members/delete",
                new { departmentIds, externalContactIds, groupIds, userIds },
                new System.Text.Json.JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            return resuls is { };
        }

        public Task<bool> AddMembersAsync(ulong groupId, List<Member> members) {
            var userIds = members.Where(x => x.type.Equals(Enums.MemberTypes.user))?.Select(u => u.id)?.ToList();
            var departmentIds = members.Where(x => x.type.Equals(Enums.MemberTypes.department))?.Select(d => d.id)?.ToList();
            var groupIds = members.Where(x => x.type.Equals(Enums.MemberTypes.group))?.Select(g => g.id)?.ToList();

            if (userIds.Count == 0) { userIds = null; }
            if (departmentIds.Count == 0) { departmentIds = null; }
            if (groupIds.Count == 0) { groupIds = null; }

            return AddMembersAsync(groupId, userIds: userIds, departmentIds: departmentIds, groupIds: groupIds);
        }
    }
}
