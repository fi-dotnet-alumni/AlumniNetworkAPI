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
				.ReverseMap();
			CreateMap<Topic, TopicCreateDTO>()
				.ReverseMap();
		}
	}
}