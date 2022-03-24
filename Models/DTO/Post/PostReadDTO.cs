namespace AlumniNetworkAPI.Models.DTO.Post
{
    public class PostReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderPictureURL { get; set; }
        public int? ReplyParentId { get; set; }
        public List<int>? Replies { get; set; }
        public int? TargetGroupId { get; set; }
        public string? GroupName { get; set; }
        public int? TargetTopicId { get; set; }
        public string? TopicName { get; set; }
    }
}
