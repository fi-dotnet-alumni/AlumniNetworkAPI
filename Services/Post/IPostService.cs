using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services
{
    public interface IPostService
    {
        public Task<IEnumerable<Post>> GetGroupAndTopicPostsAsync(int userId);
        public Task<IEnumerable<Post>> GetDirectMessagePostsAsync(int userId);
        public Task<IEnumerable<Post>> GetDirectMessagePostsFromSpecificUserAsync(int userId, int senderId);
        public Task<IEnumerable<Post>> GetPostsFromSpecificGroupAsync(int groupId);
        public Task<IEnumerable<Post>> GetPostsFromSpecificTopicAsync(int topicId);
        public Task<Post> AddPostAsync(Post post);
        public Task UpdatePostAsync(Post post);
        // temporary method for testing purposes
        public Task<IEnumerable<Post>> GetAllPostsAsync();
        public Task<Post> GetSpecificPostAsync(int postId);
        public Task<bool> PostExistsAsync(int postId);
        public Task DeletePostAsync(int postId);
    }
}
