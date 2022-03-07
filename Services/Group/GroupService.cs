using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services
{
    public class GroupService : IGroupService
    {
        public Task<Group> AddGroupAsync(Group group, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Group> GetSpecificGroupAsync(int groupId)
        {
            throw new NotImplementedException();
        }

        public bool GroupExists(int groupId)
        {
            throw new NotImplementedException();
        }

        public Task JoinGroupAsync(int groupId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
