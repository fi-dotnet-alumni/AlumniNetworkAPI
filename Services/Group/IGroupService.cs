using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services
{
    public interface IGroupService
    {
        public Task<IEnumerable<Group>> GetAllGroupsAsync();
        public Task<IEnumerable<Group>> GetUserGroupsAsync(User user);
        public Task<Group> GetSpecificGroupAsync(int groupId);
        public Task<Group> AddGroupAsync(Group group);
        public Task JoinGroupAsync(Group group, User user);
        public bool GroupExists(int groupId);
        public bool UserHasGroupAccess(Group group, User user);
    }
}
