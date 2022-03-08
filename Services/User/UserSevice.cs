using AlumniNetworkAPI.Data;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.User;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Services
{
	public class UserSevice : IUserService
	{
        private readonly IMapper _mapper;
        private readonly AlumniDbContext _context;

        public UserSevice(AlumniDbContext context, IMapper mapper)
		{
            _mapper = mapper;
            _context = context;
		}

        public Task Get()
        {
            throw new NotImplementedException();
        }

        public async Task<UserReadDTO> GetInfoAsync(int id)
        {
            try
            {
                var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
                return _mapper.Map<UserReadDTO>(foundUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<bool> UpdateAsync(UserUpdateDTO updatedUser)
        {
            // TODO: Throws error that says database profider is not set up
            // Start setting up SQL Server
            try
            {
                var updatedUserDomain = _mapper.Map<User>(updatedUser);
                _context.Entry(updatedUserDomain).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}

