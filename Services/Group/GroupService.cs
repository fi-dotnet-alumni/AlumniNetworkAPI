using Microsoft.EntityFrameworkCore;
using AlumniNetworkAPI.Data;
using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services
{
    public class GroupService : IGroupService
    {
        private readonly TemplateDBContext _context;
        public GroupService(TemplateDBContext context)
        {
            _context = context;

        }

        public Task<Group> AddGroupAsync(Group group, int userId)
        {
            // Requires GroupMember linking table
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return _context.Groups.ToList();
        }

        public async Task<Group> GetSpecificGroupAsync(int groupId)
        {
            return _context.Groups.FirstOrDefault(g => g.Id == groupId);
        }

        public bool GroupExists(int groupId)
        {
            return _context.Groups.Any(m => m.Id == groupId);
        }

        public Task JoinGroupAsync(int groupId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
