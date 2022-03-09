using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services
{
	public interface ITopicService
	{
		public Task<IEnumerable<Topic>> GetAllTopicsAsync();
		public Task<Topic> GetTopicAsync(int id);
		public Task<Topic> CreateTopicAsync(Topic topic);
		public Task JoinTopicAsync(int topicId, int userId);

	}
}

