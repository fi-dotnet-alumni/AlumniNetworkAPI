using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.Post;
using AlumniNetworkAPI.Services;
using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AlumniNetworkAPI.Controllers
{
    [Authorize]
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
        private readonly IUserService _userService;

        public PostController(IMapper mapper, IPostService postService, IGroupService groupService, IUserService userService)
        {
            _mapper = mapper;
            _postService = postService;
            _groupService = groupService;
            _userService = userService;
        }

        /// <summary>
        /// Returns all posts. Used for development, testing and debugging.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostReadDTO>>> GetAllPosts()
        {
            return _mapper.Map<List<PostReadDTO>>(await _postService.GetAllPostsAsync());
        }

        
        /// <summary>
        /// Returns a list of posts to groups and topics for which the requesting user is subscribed to.
        /// </summary>
        /// <returns></returns
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PostReadDTO>>> GetPosts()
        //{
        //    string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
        //    if (user == null)
        //    {
        //        return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
        //    }

        //    return _mapper.Map<List<PostReadDTO>>(await _postService.GetGroupAndTopicPostsAsync(user.Id));
        //}
        

        /// <summary>
        /// Return a post specified by the id if the current user is authorized to access it.
        /// </summary>
        /// <param name="id">Id of the post</param>
        /// <returns></returns
        [HttpGet("{id}")]
        public async Task<ActionResult<PostReadDTO>> GetPost(int id)
        {
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }

            Post post = await _postService.GetSpecificPostAsync(id);

            if (post == null)
            {
                // 404
                return NotFound("Post not found");
            }

            // check if the post is a direct message
            if (post.TargetUserId != null)
            {
                // check if the message was for someone else and it wasn't sent by the requesting user
                if (post.TargetUserId != user.Id && post.SenderId != user.Id)
                    return StatusCode(StatusCodes.Status403Forbidden, "You have no permission to view this post");
            }

            // check if the post belongs to a private group
            if (post.TargetGroup != null)
            {
                if (!await _groupService.UserHasGroupAccess(post.TargetGroup, user.Id))
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Missing group access");
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
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }

            return _mapper.Map<List<PostReadDTO>>(await _postService.GetDirectMessagePostsAsync(user.Id));
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
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }

            // TODO: Check if url parameter id exists (user service: UserExists(id)?)

            return _mapper.Map<List<PostReadDTO>>(await _postService.GetDirectMessagePostsFromSpecificUserAsync(user.Id, id));
        }

        /// <summary>
        /// Returns a list of posts that were sent with the group described by id as the target audience
        /// </summary>
        /// <param name="id">Id of the group</param>
        /// <returns></returns>
        [HttpGet("group/{id}")]
        public async Task<ActionResult<IEnumerable<PostReadDTO>>> GetPostsFromSpecificGroup(int id)
        {
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }

            // TODO: Check if url parameter id exists (group service: GroupExists(id)?)
            // TODO: Check if requesting user has group access (group service: UserHasGroupAccess(group, userId)

            return _mapper.Map<List<PostReadDTO>>(await _postService.GetPostsFromSpecificGroupAsync(id));
        }

        /// <summary>
        /// Returns a list of posts that were sent with the topic described by id as the target audience
        /// </summary>
        /// <param name="id">Id of the topic</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("topic/{id}")]
        public async Task<ActionResult<IEnumerable<PostReadDTO>>> GetPostsFromSpecificTopic(int id)
        {
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }

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
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }

            Post domainPost = _mapper.Map<Post>(dtoPost);

            // add requesting user as the sender
            domainPost.SenderId = user.Id;

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
            string keycloakId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userService.FindUserByKeycloakIdAsync(keycloakId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Access denied: Could not verify user.");
            }

            Post oldPost = await _postService.GetSpecificPostAsync(id);
            if (oldPost == null)
            {
                return NotFound();
            }

            if(oldPost.SenderId != user.Id)
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
