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

        public async Task<Group> AddGroupAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
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

        public async Task JoinGroupAsync(Group group, int userId)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            group.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserHasGroupAccess(Group group, int userId)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (group.Users.Contains(user))
                return true;
            return false;
        }
    }
}
