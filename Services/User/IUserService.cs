using System;
using AlumniNetworkAPI.Models.DTO.User;

namespace AlumniNetworkAPI.Services
{
	public interface IUserService
	{
		Task Get();
		Task<UserReadDTO> GetInfoAsync(int id);
		Task<bool> UpdateAsync(int id, UserUpdateDTO updatedUser);
	}
}