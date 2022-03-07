using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumniNetworkAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AlumniNetworkAPI.Extensions;
using AlumniNetworkAPI.Models.DTO.User;

namespace AlumniNetworkAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Shorthand for fetching currently authenticated user
        /// </summary>
        /// <returns>Redirects to users page</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        public IActionResult GetUser()
        {
            // TODO: Add keycloack user fetching here

            return this.SeeOther("/");
        }

        /// <summary>
        /// Gets users info with given id
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Information about the user</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserInfo(int id)
        {
            var info = await _userService.GetInfoAsync(id);
            if (info == null)
                return NotFound($"Could not find player info with id {id}");

            return Ok(info);
        }

        /// <summary>
        /// Updates user with given id
        /// </summary>
        /// <param name="id">User ID</param>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserUpdateDTO updatedUser)
        {
            bool updated = await _userService.UpdateAsync(updatedUser);
            if (updated)
                return Ok();

            return NotFound();
        }
    }
}

