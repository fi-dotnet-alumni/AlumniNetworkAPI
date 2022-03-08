using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services
{
    public interface IGroupService
    {
        public Task<IEnumerable<Group>> GetAllGroupsAsync(int userId);
        public Task<Group> GetSpecificGroupAsync(int groupId);
        public Task<Group> AddGroupAsync(Group group);
        public Task JoinGroupAsync(Group group, int userId);
        public bool GroupExists(int groupId);
        public Task<bool> UserHasGroupAccess(Group group, int userId);
    }
}
