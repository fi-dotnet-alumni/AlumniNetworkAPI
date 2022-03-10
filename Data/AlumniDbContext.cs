using System;
using AlumniNetworkAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Data
{
	public class AlumniDbContext : DbContext
	{
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Topic> Topics { get; set; }

        public AlumniDbContext(DbContextOptions options) : base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
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

            // Seeded Topic data
            modelBuilder.Entity<Topic>().HasData(
                new Topic
                {
                    Id = 1,
                    Name = ".NET",
                    Description =  "This topic covers everything .NET"
                },
                new Topic
                {
                    Id = 2,
                    Name = "JavaScript",
                    Description =  "This topic has everything JavaScript related"
                },
                new Topic
                {
                    Id = 3,
                    Name = "React",
                    Description =  "Everything React related"
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

            modelBuilder
                .Entity<Post>()
                .HasOne(p => p.Sender)
                .WithMany(p => p.Posts);

            modelBuilder
                .Entity<Topic>()
                .HasMany(t => t.Users)
                .WithMany(u => u.Topics)
                .UsingEntity(t => t.HasData(
                        new { TopicsId = 1, UsersId = 1 },
                        new { TopicsId = 2, UsersId = 1 }
                    ));
        }
    }
}

