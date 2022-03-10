using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.Topic;
using AlumniNetworkAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace AlumniNetworkAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;

        public TopicController(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns list of all available topics
        /// </summary>
        /// <returns>List of topics</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TopicReadDTO>>> GetAllTopics()
        {
            try
            {
                // TODO: Finish up after Posts are done
                // Currently posts are not being shown
                // They're also commented out on TopicReadDTO
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
        [HttpGet("/topic/{id}")]
        [Produces("application/json")]
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
        [HttpPost("/topic")]
        [Produces("application/json")]
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
        [HttpPost("/topic/{topicId}/join")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> JoinTopic(int topicId)
        {
            try
            {
                int DEBUG_UserId = 1; // TODO: Get this info from keycloak

                if(await _topicService.TopicExistsAsync(topicId))
                {
                    await _topicService.JoinTopicAsync(topicId, DEBUG_UserId);

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

