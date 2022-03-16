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
        /// <returns>Redirects to users page</returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserAsync()
        {

            return User.Identity.IsAuthenticated ? this.SeeOther("/") : NotFound("Could not authenticate user");

            //if (User.Identity.IsAuthenticated)
            //    return this.SeeOther("/");
            //return NotFound("Could not authenticate user");
        }

        /// <summary>
        /// Gets users info with given id
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Information about the user</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserReadDTO>> GetUserInfo(int id)
        {
            var info = await _userService.GetInfoAsync(id);
            if (info == null)
                return NotFound($"Could not find player info with id {id}");

            return Ok(_mapper.Map<UserReadDTO>(info));
        }

        /// <summary>
        /// Updates user with given id
        /// </summary>
        /// <param name="id">User ID</param>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserUpdateDTO updatedUser)
        {
            bool updated = await _userService.UpdateAsync(id, updatedUser);
            if (updated)
                return Ok();

            return NotFound();
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
                await _userService.AddUserAsync(newUser);
            }
            return Ok("Login successful");
        }
    }
}

