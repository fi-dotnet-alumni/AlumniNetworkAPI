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
            return _mapper.Map<List<GroupReadDTO>>(await _groupService.GetAllGroupsAsync());
        }

        /// <summary>
        /// Returns the group corresponding to the provided id.
        /// </summary>
        /// <param name="id">Id of the group</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupReadDTO>> GetGroup(int id)
        {
            Group group = await _groupService.GetSpecificGroupAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            //TODO
            // check if private
            // check if current user is a member
            // 403 otherwise

            return _mapper.Map<GroupReadDTO>(group);
        }

        /// <summary>
        /// Create a new group.
        /// </summary>
        /// <param name="dtoGroup">New group object to be created</param>
        /// <param name="userId">Id of the current user</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(GroupCreateDTO dtoGroup, int userId)
        {
            Group domainGroup = _mapper.Map<Group>(dtoGroup);
            // TODO default current user id
            domainGroup = await _groupService.AddGroupAsync(domainGroup, userId);

            return CreatedAtAction("GetGroup",
                new { id = domainGroup.Id },
                _mapper.Map<GroupReadDTO>(domainGroup));
        }

        /// <summary>
        /// Create a new group membership record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinGroup(int id, int userId)
        {
            if (!_groupService.GroupExists(id))
            {
                return NotFound();
            }

            //TODO
            // check if private
            // check if current user is a member
            // 403 otherwise

            try
            {
                await _groupService.JoinGroupAsync(id, userId);
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("Invalid user id.");
            }

            return NoContent();
        }

    }
}
