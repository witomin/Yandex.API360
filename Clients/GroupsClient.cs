using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class GroupsClient : APIClient, IGroupsClient {
        public GroupsClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<GroupsList> GetListAsync(long page = 1, long perPage = 10, CancellationToken cancellationToken = default) {
            return await Get<GroupsList>($"{_options.URLGroups}?page={page}&perPage={perPage}", cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Group>> GetListAllAsync(CancellationToken cancellationToken = default) {
            var result = new List<Group>();
            var response = await GetListAsync(1, _options.MaxResponseCount, cancellationToken).ConfigureAwait(false);
            result.AddRange(response.groups);
            var pages = response.pages;
            for (long i = 2; i <= pages; i++) {
                response = await GetListAsync(i, _options.MaxResponseCount, cancellationToken).ConfigureAwait(false);
                result.AddRange(response.groups);
            }
            return result;
        }

        public async Task<Group> AddAsync(BaseGroup group, CancellationToken cancellationToken = default) {
            if (group is null) {
                throw new ArgumentNullException(nameof(group));
            }
            return await Post<Group>($"{_options.URLGroups}", group, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(ulong groupId, CancellationToken cancellationToken = default) {
            var result = await Delete<RemovedElement>($"{_options.URLGroups}/{groupId}", cancellationToken).ConfigureAwait(false);
            return result.removed;
        }

        public async Task<bool> AddMemberAsync(long groupId, Member member, CancellationToken cancellationToken = default) {
            if (member is null) {
                throw new ArgumentNullException(nameof(member));
            }
            var result = await Post<AddedMember>($"{_options.URLGroups}/{groupId}/members", member, cancellationToken: cancellationToken).ConfigureAwait(false);
            return result.added;
        }

        public async Task<bool> DeleteMemberAsync(ulong groupId, Member member, CancellationToken cancellationToken = default) {
            var result = await Delete<DeletedMember>($"{_options.URLGroups}/{groupId}/members/{member.type}/{member.id}", cancellationToken).ConfigureAwait(false);
            return result.deleted;
        }

        public async Task<MembersList> DeleteAllMembersAsync(ulong groupId, CancellationToken cancellationToken = default) {
            var result = await Delete<MembersList>($"{_options.URLGroups}/{groupId}/members", cancellationToken).ConfigureAwait(false);
            return result;
        }

        public async Task<Group> DeleteAllManagersAsync(ulong groupId, CancellationToken cancellationToken = default) {
            var result = await Delete<Group>($"{_options.URLGroups}/{groupId}/admins", cancellationToken).ConfigureAwait(false);
            return result;
        }

        public async Task<MembersList> GetMembersAsync(ulong groupId, CancellationToken cancellationToken = default) {
            return await Get<MembersList>($"{_options.URLGroups}/{groupId}/members", cancellationToken).ConfigureAwait(false);
        }

        public async Task<Group> GetByIdAsync(ulong groupId, CancellationToken cancellationToken = default) {
            return await Get<Group>($"{_options.URLGroups}/{groupId}", cancellationToken).ConfigureAwait(false);
        }

        public async Task<Group> EditAsync(Group group, CancellationToken cancellationToken = default) {
            return await Patch<Group>($"{_options.URLGroups}/{group.id}", group as BaseGroup, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<Group> EditManagersAsync(ulong groupId, List<string> adminIds, CancellationToken cancellationToken = default) {
            return await Put<Group>($"{_options.URLGroups}/{groupId}/admins", new { adminIds }, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<Group> EditMembersAsync(ulong groupId, List<Member> members, CancellationToken cancellationToken = default) {
            return await Put<Group>($"{_options.URLGroups}/{groupId}/members", new { members }, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<MembersList2> GetMembers2Async(ulong groupId, CancellationToken cancellationToken = default) {
            return await Get<MembersList2>($"{_options.URLGroups2}/{groupId}/members", cancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> AddMembersAsync(ulong groupId, List<ulong> departmentIds = null, List<ulong> userIds = null, List<ulong> groupIds = null, List<ulong> externalContactIds = null, CancellationToken cancellationToken = default) {
            var resuls = await Patch<object>($"{_options.URLGroups2}/{groupId}/members/add",
                new { departmentIds, externalContactIds, groupIds, userIds },
                new System.Text.Json.JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull },
                cancellationToken).ConfigureAwait(false);
            return resuls is { };
        }

        public async Task<bool> DeleteMembersAsync(long groupId, List<ulong> departmentIds = null, List<ulong> userIds = null, List<ulong> groupIds = null, List<ulong> externalContactIds = null, CancellationToken cancellationToken = default) {
            var resuls = await Patch<object>($"{_options.URLGroups2}/{groupId}/members/delete",
                new { departmentIds, externalContactIds, groupIds, userIds },
                new System.Text.Json.JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }, cancellationToken).ConfigureAwait(false);
            return resuls is { };
        }

        public Task<bool> AddMembersAsync(ulong groupId, List<Member> members, CancellationToken cancellationToken = default) {
            var userIds = members.Where(x => x.type.Equals(Enums.MemberTypes.user))?.Select(u => u.id)?.ToList();
            var departmentIds = members.Where(x => x.type.Equals(Enums.MemberTypes.department))?.Select(d => d.id)?.ToList();
            var groupIds = members.Where(x => x.type.Equals(Enums.MemberTypes.group))?.Select(g => g.id)?.ToList();

            if (userIds.Count == 0) { userIds = null; }
            if (departmentIds.Count == 0) { departmentIds = null; }
            if (groupIds.Count == 0) { groupIds = null; }

            return AddMembersAsync(groupId, userIds: userIds, departmentIds: departmentIds, groupIds: groupIds, cancellationToken: cancellationToken);
        }
    }
}
