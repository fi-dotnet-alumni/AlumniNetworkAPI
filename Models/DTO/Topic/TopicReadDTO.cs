using System;
using AlumniNetworkAPI.Models.DTO.User;

namespace AlumniNetworkAPI.Models.DTO.Topic
{
	public class TopicReadDTO
	{
        public string Name { get; set; }
        public string Description { get; set; }
        //public ICollection<PostReadDTO> Posts { get; set; }
    }
}

