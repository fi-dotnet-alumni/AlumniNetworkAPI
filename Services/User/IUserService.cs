using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.User;

namespace AlumniNetworkAPI.Services
{
	public interface IUserService
	{
		Task Get();
		Task<User> GetInfoAsync(int id);
		Task<bool> UpdateAsync(int id, UserUpdateDTO updatedUser);
	}
}