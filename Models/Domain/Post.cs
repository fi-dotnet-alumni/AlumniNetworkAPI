using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlumniNetworkAPI.Models.Domain
{
	public class Post
	{
        [Key]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Title can't be more than 50 characters long.")]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }

        public int? SenderId { get; set; }
        public User? Sender { get; set; }

        public int? TargetUserId { get; set; }
        public User? TargetUser { get; set; }

        public int? TargetGroupId { get; set; }
        public Group? TargetGroup { get; set; }

        public int? TargetTopicId { get; set; }
        public Topic? TargetTopic { get; set; }
        
    }
}

