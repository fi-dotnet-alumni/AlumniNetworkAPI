using System;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.User;
using AutoMapper;

namespace AlumniNetworkAPI.Profiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserReadDTO>()
				// turn related Topics into a list of ints
				.ForMember(udto => udto.Topics, opt => opt
					.MapFrom(u => u.Topics.Select(j => j.Id).ToList()))
				// turn related Groups into a list of ints
				.ForMember(udto => udto.Groups, opt => opt
					.MapFrom(u => u.Groups.Select(j => j.Id).ToList()))
				.ReverseMap();
			CreateMap<User, UserUpdateDTO>()
				.ReverseMap();
		}
	}
}

