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
                    Name = "John Doe",
                    Status = "Attending Experis Academy courses at Noroff",
                    PictureURL = "https://picsum.photos/100/100",
                    Bio = "Patience is a virtue, and I'm learning patience. It's a tough lesson.",
                    FunFact = "When I was in college, I wanted to be involved in things that would change the world."
                },
                new User
                {
                    Id = 2,
                    Name = "Jane Doe",
                    Status = "Graduated from Experis Academy / Unemployed",
                    PictureURL = "https://cdn.icon-icons.com/icons2/1378/PNG/512/avatardefault_92824.png",
                    Bio = "I'd rather be optimistic and wrong than pessimistic and right.",
                    FunFact = "I would like to die on Mars. Just not on impact."
                }
            );

            // Seeded group data
            modelBuilder.Entity<Group>().HasData(
                new Group
                {
                    Id = 1,
                    Name = "Noroff 2021 Alumni",
                    Description = "Group for Noroff class of 2021 alumni",
                    isPrivate = false
                },
                new Group
                {
                    Id = 2,
                    Name = "Noroff 2022 Alumni",
                    Description = "Group for Noroff class of 2022 alumni",
                    isPrivate = false
                },
                new Group
                {
                    Id = 3,
                    Name = "Test Private Group",
                    Description = "A private group",
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
                },
                new Topic
                {
                    Id = 4,
                    Name = "General",
                    Description = "This topic is intended for general discussion"
                }
            );

            // Seeded Post data
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "Deploying ASP NET Core API to Azure",
                    Body = "Do you use Azure App Service? I have no clue.",
                    Timestamp = DateTime.Parse("2022-03-10T11:51:38.4490151"),
                    SenderId = 1,
                    TargetTopicId = 1
                },
                new Post
                {
                    Id = 2,
                    Title = "Deploying ASP NET Core API to Azure",
                    Body = "Yes. Do not use API Management though.",
                    Timestamp = DateTime.Parse("2022-03-10T11:53:38.4490151"),
                    SenderId = 2,
                    ReplyParentId = 1,
                },
                new Post
                {
                    Id = 3,
                    Title = "Do you need help with the deployment",
                    Body = "I can answer your questions.",
                    Timestamp = DateTime.Parse("2022-03-10T11:54:38.4490151"),
                    SenderId = 2,
                    TargetUserId = 1
                },
                new Post
                {
                    Id = 4,
                    Title = "What is the purpose of this group?",
                    Body = "I don't get it.",
                    Timestamp = DateTime.Parse("2022-03-10T11:57:38.4490151"),
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

