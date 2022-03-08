using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.DTO.Group
{
    public class GroupCreateDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name can't be more than 50 characters long.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "Description can't be more than 150 characters long.")]
        public string Description { get; set; }
        [Required]
        public bool isPrivate { get; set; }
    }
}
