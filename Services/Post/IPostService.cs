using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services
{
    public interface IPostService
    {
        public Task<IEnumerable<Post>> GetGroupAndTopicPostsAsync(int userId);
        public Task<IEnumerable<Post>> GetDirectMessagePostsAsync(int userId);
        public Task<IEnumerable<Post>> GetDirectMessagePostsFromSpecificUserAsync(int userId, int senderId);
        public Task<IEnumerable<Post>> GetGroupPostsFromSpecificGroupAsync(int userId, int groupId);
        public Task<IEnumerable<Post>> GetTopicPostsFromSpecificTopicAsync(int topicId);
        public Task<Post> AddPostAsync(Post post);
        public Task UpdatePostAsync(Post post);
    }
}
