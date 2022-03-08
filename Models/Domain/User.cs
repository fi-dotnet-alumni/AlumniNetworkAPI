using System;
namespace AlumniNetworkAPI.Models.Domain
{
	public class User
	{
        public int ID { get; set; }
        public string Name { get; set; }
        public string PictureURL { get; set; }
        public string Status { get; set; }
        public string Bio { get; set; }
        public string FunFact { get; set; }
    }
}

