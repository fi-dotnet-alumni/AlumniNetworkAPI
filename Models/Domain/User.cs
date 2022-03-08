using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlumniNetworkAPI.Models.Domain
{
	public class User
	{
        [Key]
        public int ID { get; set; }

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
    }
}

