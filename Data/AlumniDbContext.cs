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

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Tester",
                    Status = "Online",
                    PictureURL = "https://picsum.photos/100/100",
                    Bio = "Some say i might not be real at all",
                    FunFact = "What is life?"
                },
                new User
                {
                    Id = 2,
                    Name = "RandomPerson",
                    Status = "Online",
                    PictureURL = "https://cdn.icon-icons.com/icons2/1378/PNG/512/avatardefault_92824.png",
                    Bio = "Nothing to see here.",
                    FunFact = "The brand name Spam is a combination of spice and ham"
                }
            );

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

            // Seeded Post data
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "Deploying ASP NET Core API to Azure",
                    Body = "Do you use Azure App Service? I have no clue.",
                    Timestamp = DateTime.Now,
                    SenderId = 1,
                    TargetTopicId = 1
                },
                new Post
                {
                    Id = 2,
                    Title = "Deploying ASP NET Core API to Azure",
                    Body = "Yes. Do not use API Management though.",
                    Timestamp = DateTime.Now,
                    SenderId = 2,
                    ReplyParentId = 1,
                },
                new Post
                {
                    Id = 3,
                    Title = "Do you need help with the deployment",
                    Body = "I can answer your questions.",
                    Timestamp = DateTime.Now,
                    SenderId = 2,
                    TargetUserId = 1
                },
                new Post
                {
                    Id = 4,
                    Title = "What is the purpose of this group?",
                    Body = "I don't get it.",
                    Timestamp = DateTime.Now,
                    SenderId = 1,
                    TargetGroupId = 1
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

