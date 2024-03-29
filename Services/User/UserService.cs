﻿using AlumniNetworkAPI.Data;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.User;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Services
{
	public class UserSevice : IUserService
	{        
        private readonly AlumniDbContext _context;

        public UserSevice(AlumniDbContext context)
		{
            _context = context;
		}

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> FindUserByKeycloakIdAsync(string keycloakId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.KeycloakId == keycloakId);
        }

        public Task Get()
        {
            // TODO: Get current user data from keycloak

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(t => t.Topics).Include(t => t.Groups).ToListAsync();
        }

        public async Task<User> GetInfoAsync(int id)
        {
            return await _context.Users.Include(t => t.Topics).Include(t => t.Groups).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateDTO updatedUser)
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

                if (updatedUser.Status != foundUser.Status)
                    foundUser.Status = updatedUser.Status;

                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id) != null;
        }
    }
}

