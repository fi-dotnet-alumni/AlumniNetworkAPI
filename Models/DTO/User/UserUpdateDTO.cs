using System;
namespace AlumniNetworkAPI.Models.DTO.User
{
	public class UserUpdateDTO
	{
        public string Name { get; set; }
        public string PictureURL { get; set; }
        public string Status { get; set; }
        public string Bio { get; set; }
        public string FunFact { get; set; }
    }
}

