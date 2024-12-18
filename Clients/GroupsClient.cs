﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class GroupsClient : APIClient, IGroupsClient {
        public GroupsClient(Api360Options options) : base(options) { }

        public async Task<GroupsList> GetListAsync(long page = 1, long perPage = 10) {
            return await Get<GroupsList>($"{_options.URLGroups}?page={page}&perPage={perPage}");
        }

        public async Task<List<Group>> GetListAllAsync() {
            var apiResponse = await Get<GroupsList>($"{_options.URLGroups}");
            var totalGroups = apiResponse.total;
            apiResponse = await Get<GroupsList>($"{_options.URLGroups}?page={1}&perPage={totalGroups}");
            return apiResponse.groups;
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
    }
}
