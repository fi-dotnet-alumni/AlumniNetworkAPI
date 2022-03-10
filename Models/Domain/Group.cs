using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Domain
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name can't be more than 50 characters long.")]
        public string Name { get; set; }
        [MaxLength(150, ErrorMessage = "Description can't be more than 150 characters long.")]
        public string Description { get; set; }
        [Required]
        public bool isPrivate { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
