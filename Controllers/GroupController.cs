using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.Group;
using AlumniNetworkAPI.Services;
using System.Net.Mime;

namespace AlumniNetworkAPI.Controllers
{
    [Route("group")]
    [ApiController]
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
            List<GroupReadDTO> allGroups = _mapper.Map<List<GroupReadDTO>>(await _groupService.GetAllGroupsAsync(userId));
            List<GroupReadDTO> visibleGroups = new List<GroupReadDTO>();
            
            foreach (GroupReadDTO group in allGroups)
            {
                if (group.isPrivate)
                {
                    if (group.Users.Contains(userId))
                        visibleGroups.Add(group);
                }
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

            if (!await _groupService.UserHasGroupAccess(group, userId))
            {
                return StatusCode(403);
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

            domainGroup = await _groupService.AddGroupAsync(domainGroup);
            await _groupService.JoinGroupAsync(domainGroup, userId);

            return CreatedAtAction("GetGroup",
                new { id = domainGroup.Id },
                _mapper.Map<GroupReadDTO>(domainGroup));
        }

        /// <summary>
        /// Create a new group membership record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinGroup(int id)
        {
            // TODO: Check request body for user id, use current user otherwise
            // TODO: Get user id from JWT token
            int userId = 1;

            if (!_groupService.GroupExists(id))
            {
                return NotFound();
            }

            // TODO: Use user service to check if user exists before proceeding
            // Return BadRequest("Invalid user id") otherwise

            Group group = await _groupService.GetSpecificGroupAsync(id);

            if (group.isPrivate)
            {
                if (!await _groupService.UserHasGroupAccess(group, userId))
                {
                    return StatusCode(403);
                }
                else
                {
                    await _groupService.JoinGroupAsync(group, userId);
                }
            }

            return NoContent();
        }

    }
}
