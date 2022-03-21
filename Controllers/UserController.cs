using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumniNetworkAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AlumniNetworkAPI.Extensions;
using AlumniNetworkAPI.Models.DTO.User;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using AlumniNetworkAPI.Models.Domain;
using System.Security.Claims;
using System.Net.Mime;

namespace AlumniNetworkAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/user")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Shorthand for fetching currently authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        { 
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }
            return this.SeeOther($"/api/v1/user/{user.Id}");
        }

        /// <summary>
        /// Gets users info with given id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Information about the user</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUserInfo(int id)
        {
            User user = await _userService.GetInfoAsync(id);
            if (user == null)
                return NotFound($"Could not find user with id {id}");

            return Ok(_mapper.Map<UserReadDTO>(user));
        }

        /// <summary>
        /// Updates user with given id
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="updatedUser">Updated user object</param>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, UserUpdateDTO updatedUser)
        {
            if (!await _userService.UserExistsAsync(id))
            {
                return NotFound($"Could not find user with id {id}");
            }
            await _userService.UpdateAsync(id, updatedUser);
            return NoContent();
        }

        /// <summary>
        /// Tries to match token subject to an existing database user.
        /// Creates a new user if not found.
        /// </summary>
        /// <returns></returns>
        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login()
        {
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                User newUser = new User();
                newUser.Name = User.FindFirstValue("preferred_username");
                newUser.KeycloakId = keycloakId;
                newUser.PictureURL = "https://www.pngitem.com/pimgs/m/30-307416_profile-icon-png-image-free-download-searchpng-employee.png";
                await _userService.AddUserAsync(newUser);
            }
            return Ok("Login successful");
        }
    }
}

