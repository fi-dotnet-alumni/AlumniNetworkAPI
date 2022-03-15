﻿using AlumniNetworkAPI.Data;
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

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
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
            User user = await _context.Users.Include(u => u.Topics).Include(u => u.Groups).FirstOrDefaultAsync(u => u.Id == userId);
            List<Topic> userTopics = user.Topics.ToList();
            List<Group> userGroups = user.Groups.ToList();
            List<Post> allPosts = await _context.Posts.ToListAsync();
            List<Post> returnedPosts = new List<Post>();

            foreach (var post in allPosts)
            {
                if (userGroups.Contains(post.TargetGroup))
                    returnedPosts.Add(post);
                if (userTopics.Contains(post.TargetTopic))
                    returnedPosts.Add(post);
            }

            return returnedPosts;
        }

        public async Task<IEnumerable<Post>> GetPostsFromSpecificGroupAsync(int groupId)
        {
            return await _context.Posts.Where(p => p.TargetGroupId == groupId).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsFromSpecificTopicAsync(int topicId)
        {
            return await _context.Posts.Where(p => p.TargetTopicId == topicId).ToListAsync();
        }

        public async Task<Post> GetSpecificPostAsync(int postId)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
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