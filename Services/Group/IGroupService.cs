using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services
{
    public interface IGroupService
    {
        public Task<IEnumerable<Group>> GetAllGroupsAsync();
        public Task<Group> GetSpecificGroupAsync(int groupId);
        public Task<Group> AddGroupAsync(Group group, int userId);
        public Task JoinGroupAsync(int groupId, int userId);
        public bool GroupExists(int groupId);
    }
}
