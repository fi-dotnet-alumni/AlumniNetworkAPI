using System;
using AlumniNetworkAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Data
{
	public class AlumniDbContext : DbContext
	{
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

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

            // Seeded group data
            modelBuilder.Entity<Group>().HasData(
                new Group
                {
                    Id = 1,
                    Name = "Test Group 1",
                    Description = "The first test group",
                    isPrivate = false
                },
                new Group
                {
                    Id = 2,
                    Name = "Test Group 2",
                    Description = "The second test group",
                    isPrivate = false
                },
                new Group
                {
                    Id = 3,
                    Name = "Test Group 3",
                    Description = "The third test group",
                    isPrivate = true
                }
            );

            // Seeded UserGroup link table data
            modelBuilder
                .Entity<Group>()
                .HasMany(p => p.Users)
                .WithMany(p => p.Groups)
                .UsingEntity(j => j.HasData(
                    new { GroupsId = 1, UsersId = 1 },
                    new { GroupsId = 3, UsersId = 1 }
            ));
        }
    }
}

