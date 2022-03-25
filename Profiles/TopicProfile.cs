using System;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.Topic;
using AutoMapper;

namespace AlumniNetworkAPI.Profiles
{
	public class TopicProfile : Profile
	{
		public TopicProfile()
		{
			CreateMap<Topic, TopicReadDTO>()
				// turn related posts into a list of ints
				.ForMember(tdto => tdto.Posts, opt => opt
					.MapFrom(t => t.Posts.Select(p => p.Id).ToList()))
				// turn related users into a list of ints
				.ForMember(tdto => tdto.Users, opt => opt
					.MapFrom(t => t.Users.Select(u => u.Id).ToList()))
				.ReverseMap();
				
			CreateMap<Topic, TopicCreateDTO>()
				.ReverseMap();
		}
	}
}