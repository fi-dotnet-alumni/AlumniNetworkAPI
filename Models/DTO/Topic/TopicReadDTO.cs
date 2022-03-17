using System;
using AlumniNetworkAPI.Models.DTO.Post;

namespace AlumniNetworkAPI.Models.DTO.Topic
{
	public class TopicReadDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> Posts { get; set; }
    }
}

