using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.Group;
using AlumniNetworkAPI.Services;
using System.Net.Mime;

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

        public GroupController(IMapper mapper, IGroupService groupService)
        {
            _mapper = mapper;
            _groupService = groupService;
        }

        /// <summary>
        /// Returns a list of groups. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupReadDTO>>> GetGroups()
        {
            // TODO: Get user id from JWT token
            int userId = 1;

            // get all groups
            List<GroupReadDTO> allGroups = _mapper.Map<List<GroupReadDTO>>(await _groupService.GetAllGroupsAsync());
            List<GroupReadDTO> visibleGroups = new List<GroupReadDTO>();
            
            // Iterate through the groups
            foreach (GroupReadDTO group in allGroups)
            {
                // if a group is private
                if (group.isPrivate)
                {
                    // check if the requesting user is a member of the group
                    if (group.Users.Contains(userId))
                        // add the group to the list of returned groups
                        visibleGroups.Add(group);
                }
                // group is public
                else
                {
                    visibleGroups.Add(group);
                }
            }
            return visibleGroups;
        }

        /// <summary>
        /// Returns the group corresponding to the provided id.
        /// </summary>
        /// <param name="id">Id of the group</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupReadDTO>> GetGroup(int id)
        {
            // TODO: Get user id from JWT token
            int userId = 1;

            Group group = await _groupService.GetSpecificGroupAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            // check if the group is private
            if (group.isPrivate)
            {
                // check if the requesting user is not a member of the group
                if (!await _groupService.UserHasGroupAccess(group, userId))
                {
                    // return 403 Forbidden
                    return StatusCode(403);
                }
            }

            return _mapper.Map<GroupReadDTO>(group);
        }

        /// <summary>
        /// Create a new group.
        /// </summary>
        /// <param name="dtoGroup">New group object to be created</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(GroupCreateDTO dtoGroup)
        {
            Group domainGroup = _mapper.Map<Group>(dtoGroup);

            // TODO: Get user id from JWT token
            int userId = 1;

            // add the group to the database
            domainGroup = await _groupService.AddGroupAsync(domainGroup);
            // add the requesting user as the first member of the group
            await _groupService.JoinGroupAsync(domainGroup, userId);

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
        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinGroup(int id, [FromBody] int userId = default)
        {
            // TODO: Get user id from JWT token
            int requestingUserId = 1;
            // TODO: Check request body for user id, use requesting user otherwise
            if (userId == default)
            {
                userId = requestingUserId;
            }

            // invalid group id
            if (!_groupService.GroupExists(id))
            {
                return NotFound();
            }

            // TODO: Use user service to check if user exists before proceeding
            // Return BadRequest("Invalid user id") otherwise

            Group group = await _groupService.GetSpecificGroupAsync(id);

            // check if the group is private
            if (group.isPrivate)
            {
                // check if the requesting user is not a memeber of the group
                if (!await _groupService.UserHasGroupAccess(group, requestingUserId))
                {
                    // 403 Forbidden
                    return StatusCode(403);
                }
                else
                {
                    // add the specified user to the group
                    await _groupService.JoinGroupAsync(group, userId);
                }
            }

            return NoContent();
        }

    }
}
