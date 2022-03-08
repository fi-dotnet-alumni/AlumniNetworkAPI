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
            // TODO: Get current user data from keycloak

            throw new NotImplementedException();
        }

        public async Task<UserReadDTO> GetInfoAsync(int id)
        {
            try
            {
                var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                return _mapper.Map<UserReadDTO>(foundUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateDTO updatedUser)
        {
            try
            {
                var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (foundUser != null)
                {
                    if (updatedUser.Bio != foundUser.Bio)
                        foundUser.Bio = updatedUser.Bio;

                    if (updatedUser.FunFact != foundUser.FunFact)
                        foundUser.FunFact = updatedUser.FunFact;

                    if (updatedUser.PictureURL != foundUser.PictureURL)
                        foundUser.PictureURL = updatedUser.PictureURL;

                    if (updatedUser.Name != foundUser.Name)
                        foundUser.Name = updatedUser.Name;

                    return await _context.SaveChangesAsync() > 0;
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}

