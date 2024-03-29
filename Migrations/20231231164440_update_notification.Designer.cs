﻿// <auto-generated />
using System;
using Clone_Main_Project_0710.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HHSocialNetwork_Project.Migrations
{
    [DbContext(typeof(SocialContext))]
    [Migration("20231231164440_update_notification")]
    partial class update_notification
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Clone_Main_Project_0710.Models.CommentDetail", b =>
                {
                    b.Property<Guid>("CommentDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .HasMaxLength(2147483647)
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageComment")
                        .HasMaxLength(2147483647)
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ViewerCommentId")
                        .HasColumnType("char(36)");

                    b.HasKey("CommentDetailId");

                    b.HasIndex("ViewerCommentId");

                    b.ToTable("CommentDetails");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.Notification", b =>
                {
                    b.Property<Guid>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Read")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("SourceId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("TargetId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("UserPostId")
                        .HasColumnType("char(36)");

                    b.HasKey("NotificationId");

                    b.HasIndex("SourceId");

                    b.HasIndex("TargetId");

                    b.HasIndex("UserPostId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("GenderName")
                        .HasColumnType("longtext");

                    b.Property<string>("Intro")
                        .HasMaxLength(3000)
                        .HasColumnType("varchar(3000)");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("NumberPhone")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Profile")
                        .HasMaxLength(30000)
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("RegisterAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<bool?>("role")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.UserFollower", b =>
                {
                    b.Property<Guid>("FollowerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("char(36)");

                    b.Property<string>("TypeFlower")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("FollowerId");

                    b.HasIndex("TargetId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFollowers");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.UserImage", b =>
                {
                    b.Property<Guid>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageData")
                        .HasMaxLength(2147483647)
                        .HasColumnType("longtext");

                    b.Property<string>("ImageName")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAvatar")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsCoverImage")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("UserPostId")
                        .HasColumnType("char(36)");

                    b.HasKey("ImageId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserPostId");

                    b.ToTable("UserImages");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.UserPost", b =>
                {
                    b.Property<Guid>("UserPostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Comments")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasMaxLength(3000)
                        .HasColumnType("varchar(3000)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("HasImage")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("Privacy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserPostId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPosts");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.ViewerComment", b =>
                {
                    b.Property<Guid>("ViewerCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("CommentsCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserPostId")
                        .HasColumnType("char(36)");

                    b.HasKey("ViewerCommentId");

                    b.HasIndex("SenderId");

                    b.HasIndex("UserPostId");

                    b.ToTable("ViewerComments");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.ViewerLike", b =>
                {
                    b.Property<Guid>("ViewerLikeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("LikePost")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserPostId")
                        .HasColumnType("char(36)");

                    b.HasKey("ViewerLikeId");

                    b.HasIndex("SenderId");

                    b.HasIndex("UserPostId");

                    b.ToTable("ViewerLikes");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.tblDisable", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Disable")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LogTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId");

                    b.ToTable("tblDisables");
                });

            modelBuilder.Entity("HHSocialNetwork_Project.Models.UserFriend", b =>
                {
                    b.Property<Guid>("UserFriendId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsFriend")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("char(36)");

                    b.Property<string>("TypeFriend")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserFriendId");

                    b.HasIndex("SourceId");

                    b.HasIndex("TargetId");

                    b.ToTable("UserFriends");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.CommentDetail", b =>
                {
                    b.HasOne("Clone_Main_Project_0710.Models.ViewerComment", "ViewerComment")
                        .WithMany("CommentDetails")
                        .HasForeignKey("ViewerCommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ViewerComment");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.Notification", b =>
                {
                    b.HasOne("Clone_Main_Project_0710.Models.User", "SourceUser")
                        .WithMany("SourceNotifications")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Clone_Main_Project_0710.Models.User", "TargetUser")
                        .WithMany("TargetNotifications")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Clone_Main_Project_0710.Models.UserPost", "UserPost")
                        .WithMany("Notifications")
                        .HasForeignKey("UserPostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("SourceUser");

                    b.Navigation("TargetUser");

                    b.Navigation("UserPost");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.UserFollower", b =>
                {
                    b.HasOne("Clone_Main_Project_0710.Models.User", "TargetUser")
                        .WithMany("TargetUserFollowers")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clone_Main_Project_0710.Models.User", "SourceUser")
                        .WithMany("SourceUserFollowers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SourceUser");

                    b.Navigation("TargetUser");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.UserImage", b =>
                {
                    b.HasOne("Clone_Main_Project_0710.Models.User", "User")
                        .WithMany("UserImages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Clone_Main_Project_0710.Models.UserPost", "UserPost")
                        .WithMany("UserImages")
                        .HasForeignKey("UserPostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");

                    b.Navigation("UserPost");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.UserPost", b =>
                {
                    b.HasOne("Clone_Main_Project_0710.Models.User", "User")
                        .WithMany("UserPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.ViewerComment", b =>
                {
                    b.HasOne("Clone_Main_Project_0710.Models.User", "User")
                        .WithMany("ViewerComments")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clone_Main_Project_0710.Models.UserPost", "UserPost")
                        .WithMany("ViewerComments")
                        .HasForeignKey("UserPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserPost");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.ViewerLike", b =>
                {
                    b.HasOne("Clone_Main_Project_0710.Models.User", "User")
                        .WithMany("ViewerLikes")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clone_Main_Project_0710.Models.UserPost", "UserPost")
                        .WithMany("ViewerLikes")
                        .HasForeignKey("UserPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserPost");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.tblDisable", b =>
                {
                    b.HasOne("Clone_Main_Project_0710.Models.User", "User")
                        .WithMany("TblDisables")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HHSocialNetwork_Project.Models.UserFriend", b =>
                {
                    b.HasOne("Clone_Main_Project_0710.Models.User", "SourceUser")
                        .WithMany("SourceUserFriends")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clone_Main_Project_0710.Models.User", "TargetUser")
                        .WithMany("TargetUserFriends")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SourceUser");

                    b.Navigation("TargetUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.User", b =>
                {
                    b.Navigation("SourceNotifications");

                    b.Navigation("SourceUserFollowers");

                    b.Navigation("SourceUserFriends");

                    b.Navigation("TargetNotifications");

                    b.Navigation("TargetUserFollowers");

                    b.Navigation("TargetUserFriends");

                    b.Navigation("TblDisables");

                    b.Navigation("UserImages");

                    b.Navigation("UserPosts");

                    b.Navigation("ViewerComments");

                    b.Navigation("ViewerLikes");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.UserPost", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("UserImages");

                    b.Navigation("ViewerComments");

                    b.Navigation("ViewerLikes");
                });

            modelBuilder.Entity("Clone_Main_Project_0710.Models.ViewerComment", b =>
                {
                    b.Navigation("CommentDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
