using Microsoft.EntityFrameworkCore;
using AlumniNetworkAPI.Data;
using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services
{
    public class GroupService : IGroupService
    {
        private readonly AlumniDbContext _context;
        public GroupService(AlumniDbContext context)
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
            return await _context.Groups.Include(g => g.Users).Include(g => g.Posts).ToListAsync();
        }

        public async Task<Group> GetSpecificGroupAsync(int groupId)
        {
            return await _context.Groups.Include(g => g.Users).Include(g => g.Posts).FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public bool GroupExists(int groupId)
        {
            return _context.Groups.Any(g => g.Id == groupId);
        }

        public Task JoinGroupAsync(int groupId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
