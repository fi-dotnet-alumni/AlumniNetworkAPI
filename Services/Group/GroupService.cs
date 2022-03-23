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

        public async Task<IEnumerable<Group>> GetUserGroupsAsync(User user)
        {
            List<Group> allGroups = await _context.Groups.Include(g => g.Users).Include(g => g.Posts).ToListAsync();
            List<Group> returnedGroups = new List<Group>();

            foreach (Group group in allGroups)
            {
                if (group.isPrivate)
                {
                    if (group.Users.Contains(user))
                        returnedGroups.Add(group);
                }
                // public group
                else
                {
                    returnedGroups.Add(group);
                }
            }
            return returnedGroups.OrderByDescending(g => g.Posts.Count()).ToList();
        }

        public bool GroupExists(int groupId)
        {
            return _context.Groups.Any(g => g.Id == groupId);
        }

        public async Task JoinGroupAsync(Group group, User user)
        {
            group.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public bool UserHasGroupAccess(Group group, User user)
        {
            if (group.isPrivate)
            {
                if (group.Users.Contains(user))
                    return true;
                else
                    return false;
            }
            return true;
        }
    }
}
