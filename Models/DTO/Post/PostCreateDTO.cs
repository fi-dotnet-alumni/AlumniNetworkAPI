namespace AlumniNetworkAPI.Models.DTO.Post
{
    public class PostCreateDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int? ReplyParentId { get; set; }
        public int? TargetUserId { get; set; }
        public int? TargetGroupId { get; set; }
        public int? TargetTopicId { get; set; }

    }
}
