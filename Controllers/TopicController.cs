using System.Net.Mime;
using System.Security.Claims;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.Topic;
using AlumniNetworkAPI.Services;
using AutoMapper;
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TopicReadDTO>>> GetAllTopics()
        {
            try
            {
                var topics = await _topicService.GetAllTopicsAsync();
                return Ok(_mapper.Map<IEnumerable<TopicReadDTO>>(topics));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Gets topic with given id
        /// </summary>
        /// <param name="id">Topic id</param>
        /// <returns>Found Topic</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TopicReadDTO>> GetTopic(int id)
        {
            try
            {
                var topic = await _topicService.GetTopicAsync(id);
                if(topic != null)
                    return Ok(_mapper.Map<TopicReadDTO>(topic));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Create new topic
        /// </summary>
        /// <param name="newTopic">New topic object</param>
        /// <returns>Created topic</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTopic([FromBody] TopicCreateDTO newTopic)
        {
            try
            {
                var createdTopic = await _topicService.CreateTopicAsync(_mapper.Map<Topic>(newTopic));
                if(createdTopic != null)
                    return CreatedAtAction("CreateTopic", _mapper.Map<TopicReadDTO>(createdTopic));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Join/Subscribe to topic
        /// </summary>
        /// <param name="topicId">Topic id</param>
        [HttpPost("{id}/join")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> JoinTopic(int topicId)
        {
            try
            {
                string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
                }

                if (await _topicService.TopicExistsAsync(topicId))
                {
                    await _topicService.JoinTopicAsync(topicId, user.Id);

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }
    }
}

