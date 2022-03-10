using AlumniNetworkAPI.Data;
using AlumniNetworkAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Services
{
    public class PostService : IPostService
    {
        private readonly AlumniDbContext _context;
        public PostService(AlumniDbContext context)
        {
            _context = context;
        }
        public async Task<Post> AddPostAsync(Post post)
        {
            post.Timestamp = DateTime.Now;
            _context.Posts.Add(post);
            if (post.ReplyParentId != null)
            {
                Post parentPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == post.ReplyParentId);
                if (parentPost != null)
                {
                    parentPost.Replies.Add(post);
                }
            }
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<IEnumerable<Post>> GetDirectMessagePostsAsync(int userId)
        {
            return await _context.Posts.Where(p => p.TargetUserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetDirectMessagePostsFromSpecificUserAsync(int userId, int senderId)
        {
            return await _context.Posts.Where(p => p.TargetUserId == userId && p.SenderId == senderId).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetGroupAndTopicPostsAsync(int userId)
        {
            // find user
            // list of user topic ids
            // list of user group ids
            // return posts where target group id or target topic id matches
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetGroupPostsFromSpecificGroupAsync(int userId, int groupId)
        {
            // check group access here or in controller?
            return await _context.Posts.Where(p => p.TargetGroupId == groupId).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetTopicPostsFromSpecificTopicAsync(int topicId)
        {
            return await _context.Posts.Where(p => p.TargetTopicId == topicId).ToListAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            // should timestamp be modified? effects on the order of posts?
            post.Timestamp = DateTime.Now;
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
