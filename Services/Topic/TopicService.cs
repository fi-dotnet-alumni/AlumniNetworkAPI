using System;
using AlumniNetworkAPI.Data;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.Topic;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Services
{
    public class TopicService : ITopicService
    {
        private readonly AlumniDbContext _context;

        public TopicService(AlumniDbContext context)
        {
            _context = context;
        }

        public async Task<Topic> CreateTopicAsync(Topic topic)
        {
            await _context.Topics.AddAsync(topic);
            await _context.SaveChangesAsync();
            return topic;
        }

        public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
        {
            return await _context.Topics.Include(t => t.Posts).ToListAsync();
        }

        public async Task<Topic> GetTopicAsync(int id)
        {
            return await _context.Topics.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task JoinTopicAsync(int topicId, int userId)
        {
            var foundTopic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == topicId);
            if(foundTopic != null)
            {
                var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if(foundUser != null)
                {
                    foundTopic.Users.Add(foundUser);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}

