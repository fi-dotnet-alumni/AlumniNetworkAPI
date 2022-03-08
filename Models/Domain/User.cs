using System;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Domain
{
	public class User
	{
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(12)]
        public string Name { get; set; }

        [Url]
        public string PictureURL { get; set; }

        public string Status { get; set; }

        [MaxLength(512)]
        public string Bio { get; set; }

        [MaxLength(256)]
        public string FunFact { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}