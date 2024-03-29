﻿// <auto-generated />
using System;
using AlumniNetworkAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    [DbContext(typeof(AlumniDbContext))]
    [Migration("20220308132540_GroupsTestData")]
    partial class GroupsTestData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("isPrivate")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The first test group",
                            Name = "Test Group 1",
                            isPrivate = false
                        },
                        new
                        {
                            Id = 2,
                            Description = "The second test group",
                            Name = "Test Group 2",
                            isPrivate = false
                        },
                        new
                        {
                            Id = 3,
                            Description = "The third test group",
                            Name = "Test Group 3",
                            isPrivate = true
                        });
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SenderId")
                        .HasColumnType("int");

                    b.Property<int?>("TargeTopicId")
                        .HasColumnType("int");

                    b.Property<int?>("TargetGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("TargetTopicId")
                        .HasColumnType("int");

                    b.Property<int?>("TargetUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.HasIndex("TargeTopicId");

                    b.HasIndex("TargetGroupId");

                    b.HasIndex("TargetUserId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Topic");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Bio")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("FunFact")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("PictureURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bio = "Some say i might not be real at all",
                            FunFact = "What is life?",
                            Name = "Tester",
                            PictureURL = "https://picsum.photos/100/100",
                            Status = "Online"
                        });
                });

            modelBuilder.Entity("GroupUser", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("GroupsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("GroupUser");

                    b.HasData(
                        new
                        {
                            GroupsId = 1,
                            UsersId = 1
                        },
                        new
                        {
                            GroupsId = 3,
                            UsersId = 1
                        });
                });

            modelBuilder.Entity("TopicUser", b =>
                {
                    b.Property<int>("TopicsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("TopicsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("TopicUser");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Post", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Domain.User", "Sender")
                        .WithMany("Posts")
                        .HasForeignKey("SenderId");

                    b.HasOne("AlumniNetworkAPI.Models.Domain.Topic", "TargeTopic")
                        .WithMany("Posts")
                        .HasForeignKey("TargeTopicId");

                    b.HasOne("AlumniNetworkAPI.Models.Domain.Group", "TargetGroup")
                        .WithMany("Posts")
                        .HasForeignKey("TargetGroupId");

                    b.HasOne("AlumniNetworkAPI.Models.Domain.User", "TargetUser")
                        .WithMany()
                        .HasForeignKey("TargetUserId");

                    b.Navigation("Sender");

                    b.Navigation("TargeTopic");

                    b.Navigation("TargetGroup");

                    b.Navigation("TargetUser");
                });

            modelBuilder.Entity("GroupUser", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Domain.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopicUser", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Domain.Topic", null)
                        .WithMany()
                        .HasForeignKey("TopicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Group", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Topic", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.User", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
