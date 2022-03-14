using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.Post;
using AlumniNetworkAPI.Services;
using System.Net.Mime;

namespace AlumniNetworkAPI.Controllers
{
    [ApiController]
    [Route("api/v1/post")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PostController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly IGroupService _groupService;

        public PostController(IMapper mapper, IPostService postService)
        {
            _mapper = mapper;
            _postService = postService;
        }

        /// <summary>
        /// Returns all posts. Change to user topic and group posts later.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostReadDTO>>> GetAllPosts()
        {
            return _mapper.Map<List<PostReadDTO>>(await _postService.GetAllPostsAsync());
        }

        /// <summary>
        /// Return a post specified by the id if the current user is authorized to access it.
        /// </summary>
        /// <param name="id">Id of the post</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PostReadDTO>> GetPost(int id)
        {
            // TODO: Get user id from JWT token
            int userId = 1;

            Post post = await _postService.GetSpecificPostAsync(id);

            if (post == null)
            {
                // 404
                return NotFound();
            }

            // check if the post is a direct message
            if (post.TargetUserId != null)
            {
                // check if the message was for someone else and it wasn't sent by the requesting user
                if (post.TargetUserId != userId && post.SenderId != userId)
                    return StatusCode(403);
            }

            // check if the post belongs to a private group
            if (post.TargetGroup != null)
            {
                if (!await _groupService.UserHasGroupAccess(post.TargetGroup, userId))
                {
                    return StatusCode(403);
                }
            }
            
            return _mapper.Map<PostReadDTO>(post);
        }

        /// <summary>
        /// Returns a list of posts that were sent as direct messages to the requesting user.
        /// </summary>
        /// <returns></returns>
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<PostReadDTO>>> GetDirectMessages()
        {
            // TODO: Get user id from JWT token
            int userId = 1;

            return _mapper.Map<List<PostReadDTO>>(await _postService.GetDirectMessagePostsAsync(userId));
        }

        /// <summary>
        /// Returns a list of posts that were sent as direct messages to the requesting user from the 
        /// specific user described by the id
        /// </summary>
        /// <param name="id">Id of the sender</param>
        /// <returns></returns>
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<PostReadDTO>>> GetDirectMessagesFromUser(int id)
        {
            // TODO: Get user id from JWT token
            int userId = 1;

            // TODO: Check if url parameter id exists (user service: UserExists(id)?)

            return _mapper.Map<List<PostReadDTO>>(await _postService.GetDirectMessagePostsFromSpecificUserAsync(userId, id));
        }

        /// <summary>
        /// Returns a list of posts that were sent with the group described by id as the target audience
        /// </summary>
        /// <param name="id">Id of the group</param>
        /// <returns></returns>
        [HttpGet("group/{id}")]
        public async Task<ActionResult<IEnumerable<PostReadDTO>>> GetPostsFromSpecificGroup(int id)
        {
            // TODO: Get user id from JWT token
            int userId = 1;

            // TODO: Check if url parameter id exists (group service: GroupExists(id)?)
            // TODO: Check if requesting user has group access (group service: UserHasGroupAccess(group, userId)

            return _mapper.Map<List<PostReadDTO>>(await _postService.GetPostsFromSpecificGroupAsync(id));
        }

        /// <summary>
        /// Returns a list of posts that were sent with the topic described by id as the target audience
        /// </summary>
        /// <param name="id">Id of the topic</param>
        /// <returns></returns>
        [HttpGet("topic/{id}")]
        public async Task<ActionResult<IEnumerable<PostReadDTO>>> GetPostsFromSpecificTopic(int id)
        {
            // TODO: Get user id from JWT token
            int userId = 1;

            // TODO: Check if url parameter id exists (topic service: TopicExists(id)?)

            return _mapper.Map<List<PostReadDTO>>(await _postService.GetPostsFromSpecificTopicAsync(id));
        }

        /// <summary>
        /// Create a new post.
        /// </summary>
        /// <param name="dtoPost">New post object to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(PostCreateDTO dtoPost)
        {
            // TODO: Get user id from JWT token
            int userId = 1;

            Post domainPost = _mapper.Map<Post>(dtoPost);

            // add requesting user as the sender
            domainPost.SenderId = userId;

            // make sure post has only one target audience
            // TODO: Check if Ids are valid since otherwise the foreign keys will cause an error
            int targetsSpecified = 0;
            if (dtoPost.ReplyParentId != null)
                targetsSpecified++;
            if (dtoPost.TargetTopicId != null)
                targetsSpecified++;
            if (dtoPost.TargetUserId != null)
                targetsSpecified++;
            if (dtoPost.TargetGroupId != null)
                targetsSpecified++;

            if (targetsSpecified > 1 || targetsSpecified == 0)
            {
                return BadRequest();
            }

            if (dtoPost.TargetGroupId.HasValue)
            {
                // TODO: Check if user has group access in case of private group
                // return 403 Forbidden if not
            }

            domainPost = await _postService.AddPostAsync(domainPost);

            return Ok(_mapper.Map<PostReadDTO>(domainPost));
        }

        /// <summary>
        /// Update an existing post.
        /// </summary>
        /// <param name="id">Id of the post</param>
        /// <param name="dtoPost">Post object with updated data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, PostEditDTO dtoPost)
        {
            // TODO: Get user id from JWT token
            int userId = 1;

            Post oldPost = await _postService.GetSpecificPostAsync(id);
            if (oldPost == null)
            {
                return NotFound();
            }

            if(oldPost.SenderId != userId)
            {
                return StatusCode(403);
            }

            oldPost.Title = dtoPost.Title;
            oldPost.Body = dtoPost.Body;
            await _postService.UpdatePostAsync(oldPost);

            return NoContent();
        }
    }
}
