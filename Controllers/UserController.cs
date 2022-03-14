using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumniNetworkAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AlumniNetworkAPI.Extensions;
using AlumniNetworkAPI.Models.DTO.User;
using AutoMapper;

namespace AlumniNetworkAPI.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
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
        [Produces("application/json")]
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
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserUpdateDTO updatedUser)
        {
            bool updated = await _userService.UpdateAsync(id, updatedUser);
            if (updated)
                return Ok();

            return NotFound();
        }
    }
}

