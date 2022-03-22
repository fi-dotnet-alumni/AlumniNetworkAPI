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
            post.Timestamp = DateTime.UtcNow;
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

        public async Task DeletePostAsync(int postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            await RemoveReplies(post.Id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(p => p.Sender).OrderByDescending(p => p.Timestamp).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetDirectMessagePostsAsync(int userId)
        {
            return await _context.Posts.Include(p => p.Sender).Where(p => p.TargetUserId == userId).OrderByDescending(p => p.Timestamp).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetDirectMessagePostsFromSpecificUserAsync(int userId, int senderId)
        {
            return await _context.Posts.Include(p => p.Sender).Where(p => p.TargetUserId == userId && p.SenderId == senderId).OrderByDescending(p => p.Timestamp).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetGroupAndTopicPostsAsync(int userId)
        {
            User user = await _context.Users.Include(u => u.Topics).Include(u => u.Groups).FirstOrDefaultAsync(u => u.Id == userId);
            List<Topic> userTopics = user.Topics.ToList();
            List<Group> userGroups = user.Groups.ToList();
            List<Post> allPosts = await _context.Posts.Include(p => p.Sender).ToListAsync();
            List<Post> returnedPosts = new List<Post>();

            foreach (var post in allPosts)
            {
                if (userGroups.Contains(post.TargetGroup))
                    returnedPosts.Add(post);
                if (userTopics.Contains(post.TargetTopic))
                    returnedPosts.Add(post);
            }

            return returnedPosts.OrderByDescending(p => p.Timestamp);
        }

        public async Task<IEnumerable<Post>> GetPostsFromSpecificGroupAsync(int groupId)
        {
            return await _context.Posts.Include(p => p.Sender).Where(p => p.TargetGroupId == groupId).OrderByDescending(p => p.Timestamp).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsFromSpecificTopicAsync(int topicId)
        {
            return await _context.Posts.Include(p => p.Sender).Where(p => p.TargetTopicId == topicId).OrderByDescending(p => p.Timestamp).ToListAsync();
        }

        public async Task<Post> GetSpecificPostAsync(int postId)
        {
            return await _context.Posts.Include(p => p.Sender).FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<bool> PostExistsAsync(int postId)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId) != null;
        }

        public async Task RemoveReplies(int postId)
        {
            var replies = await _context.Posts.Where(p => p.ReplyParentId == postId).ToListAsync();
            foreach (var reply in replies)
            {
                await RemoveReplies(reply.Id);
                _context.Posts.Remove(reply);
            }
        }

        public async Task UpdatePostAsync(Post post)
        {
            // should timestamp be modified? effects on the order of posts?
            post.Timestamp = DateTime.UtcNow;
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
