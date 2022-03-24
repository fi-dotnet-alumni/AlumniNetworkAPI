using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.Group;
using AlumniNetworkAPI.Services;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AlumniNetworkAPI.Controllers
{
    [ApiController]
    [Route("api/v1/group")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class GroupController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;

        public GroupController(IMapper mapper, IGroupService groupService, IUserService userService)
        {
            _mapper = mapper;
            _groupService = groupService;
            _userService = userService;
        }

        /// <summary>
        /// Returns a list of groups that the user has access to. 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupReadDTO>>> GetUserGroups()
        {
            // extract subject from token and find corresponding user
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }

            return _mapper.Map<List<GroupReadDTO>>(await _groupService.GetUserGroupsAsync(user));
        }

        /// <summary>
        /// Returns the group corresponding to the provided id.
        /// </summary>
        /// <param name="id">Id of the group</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupReadDTO>> GetGroup(int id)
        {
            // extract subject from token and find corresponding user
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }

            Group group = await _groupService.GetSpecificGroupAsync(id);

            if (group == null)
            {
                return NotFound($"Group does not exist with id {id}");
            }

            // check if the requesting user has access to the group
            if (!_groupService.UserHasGroupAccess(group, user))
            {
                // return 403 Forbidden
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: User does not have access to group");
            }
            
            return _mapper.Map<GroupReadDTO>(group);
        }

        /// <summary>
        /// Create a new group.
        /// </summary>
        /// <param name="dtoGroup">New group object to be created</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(GroupCreateDTO dtoGroup)
        {
            // extract subject from token and find corresponding user
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }

            Group domainGroup = _mapper.Map<Group>(dtoGroup);

            // add the group to the database
            domainGroup = await _groupService.AddGroupAsync(domainGroup);
            // add the requesting user as the first member of the group
            await _groupService.JoinGroupAsync(domainGroup, user);

            return CreatedAtAction("GetGroup",
                new { id = domainGroup.Id },
                _mapper.Map<GroupReadDTO>(domainGroup));
        }

        /// <summary>
        /// Create a new group membership record.
        /// </summary>
        /// <param name="id">Id of the group</param>
        /// <param name="userId">Optional id of the joining user in request body</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinGroup(int id, [FromBody] int? userId = null)
        {
            // extract subject from token and find corresponding user
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User requestingUser = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (requestingUser == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }
            User joiningUser = new User();

            // check request body for user id, use requesting user otherwise
            if (userId == null)
            {
                joiningUser = requestingUser;
            }
            // user id provided in request body
            else
            {
                if (!await _userService.UserExistsAsync(userId.Value))
                {
                    return BadRequest("Invalid user id provided.");
                }
                joiningUser = await _userService.GetInfoAsync(userId.Value);
            }

            // invalid group id
            if (!_groupService.GroupExists(id))
            {
                return NotFound($"Group does not exist with id {id}");
            }

            Group group = await _groupService.GetSpecificGroupAsync(id);

            // check if the requesting user is not a member of the group
            if (!_groupService.UserHasGroupAccess(group, requestingUser))
            {
                // 403 Forbidden
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: User does not have access to group");
            }
            // check if the user is already in the group
            if (_groupService.UserIsAGroupMember(group, joiningUser))
            {
                // 400 Bad request
                return StatusCode(StatusCodes.Status400BadRequest, "User is already a member of the group");
            }
            else
            {
                // add the specified user to the group
                await _groupService.JoinGroupAsync(group, joiningUser);
            }

            return NoContent();
        }

        /// <summary>
        /// Removes the requesting user from the group specified by the id
        /// </summary>
        /// <param name="id">Id of the group</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{id}/leave")]
        public async Task<IActionResult> LeaveGroup(int id)
        {
            // extract subject from token and find corresponding user
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }

            // invalid group id
            if (!_groupService.GroupExists(id))
            {
                return NotFound($"Group does not exist with id {id}");
            }

            Group group = await _groupService.GetSpecificGroupAsync(id);

            // check if the requesting user is not a member of the group
            if (!_groupService.UserIsAGroupMember(group, user))
            {
                // 400 Bad request
                return StatusCode(StatusCodes.Status400BadRequest, "User is not a member of the group");
            }
            else
            {
                // remove the requesting user from the group
                await _groupService.LeaveGroupAsync(group, user);
            }

            return NoContent();
        }

    }
}
