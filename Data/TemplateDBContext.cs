using System;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.User;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Data
{
	public class TemplateDBContext : DbContext
	{
		// TODO: Replace this data wih actual data from database
		public List<User> Users = new List<User>
		{
			new User { ID = 0, Name = "Tester", PictureURL = "url", Status = "Online", Bio = "I am made for testing!", FunFact = "Some say i might not even be real" }
		};

		public List<Group> Groups = new List<Group>
		{
			new Group{ Id = 1, Name = "Test Group 1", Description = "The first test group", isPrivate = false },
			new Group{ Id = 2, Name = "Test Group 2", Description = "The second test group", isPrivate = false}
		};

        public TemplateDBContext(DbContextOptions options) : base(options)
        {

        }
	}
}

