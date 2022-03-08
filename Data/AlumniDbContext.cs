using System;
using AlumniNetworkAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Data
{
	public class AlumniDbContext : DbContext
	{
        public DbSet<User> Users { get; set; }

        public AlumniDbContext(DbContextOptions options) : base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                ID = 1,
                Name = "Tester",
                Status = "Online",
                PictureURL = "https://picsum.photos/100/100",
                Bio = "Some say i might not be real at all",
                FunFact = "What is life?"
            });
        }
    }
}

