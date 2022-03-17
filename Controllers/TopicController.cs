using System.Net.Mime;
using System.Security.Claims;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.Topic;
using AlumniNetworkAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AlumniNetworkAPI.Controllers
{
    [ApiController]
    [Route("api/v1/topic")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public TopicController(ITopicService topicService, IUserService userService, IMapper mapper)
        {
            _topicService = topicService;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns list of all available topics
        /// </summary>
        /// <returns>List of topics</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicReadDTO>>> GetAllTopics()
        {
            var topics = await _topicService.GetAllTopicsAsync();
            return Ok(_mapper.Map<IEnumerable<TopicReadDTO>>(topics));
        }

        /// <summary>
        /// Gets topic with given id
        /// </summary>
        /// <param name="id">Topic id</param>
        /// <returns>Found Topic</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicReadDTO>> GetTopic(int id)
        {
            var topic = await _topicService.GetTopicAsync(id);
            if(topic == null)
            {
                return NotFound($"Topic does not exist with id {id}");
            }
            
            return Ok(_mapper.Map<TopicReadDTO>(topic));
            
            
        }

        /// <summary>
        /// Create new topic
        /// </summary>
        /// <param name="newTopic">New topic object</param>
        /// <returns>Created topic</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTopic(TopicCreateDTO newTopic)
        {
            var createdTopic = await _topicService.CreateTopicAsync(_mapper.Map<Topic>(newTopic));

            return CreatedAtAction("GetTopic", new { id = createdTopic.Id }, _mapper.Map<TopicReadDTO>(createdTopic));
        }

        /// <summary>
        /// Join/Subscribe to topic
        /// </summary>
        /// <param name="id">Topic id</param>
        [Authorize]
        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinTopic(int id)
        {
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }

            if (!await _topicService.TopicExistsAsync(id))
            {
                return NotFound($"Topic does not exist with id {id}");
            }

            await _topicService.JoinTopicAsync(id, user.Id);

            return NoContent();
        }
    }
}

