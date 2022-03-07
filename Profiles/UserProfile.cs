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
				.ReverseMap();
			CreateMap<User, UserUpdateDTO>()
				.ReverseMap();
		}
	}
}

