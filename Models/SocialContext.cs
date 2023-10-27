using System.Reflection.Metadata;
using HHSocialNetwork_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clone_Main_Project_0710.Models
{
        public class SocialContext : IdentityDbContext
        {

        public SocialContext(DbContextOptions<SocialContext> options) : base(options) { }
                public DbSet<User> Users { get; set; }
                public DbSet<ViewerComment> ViewerComments { get; set; }
                public DbSet<CommentDetail> CommentDetails { get; set; }
                public DbSet<UserFollower> UserFollowers { get; set; }
                public DbSet<UserPost> UserPosts { get; set; }
                public DbSet<ViewerLike> ViewerLikes { get; set; }
                public DbSet<UserFriend> UserFriends { get; set; }
                public DbSet<UserImage> UserImages { get; set; }

                protected override void OnModelCreating(ModelBuilder builder)
                {
                        base.OnModelCreating(builder);

                        builder.Entity<UserFollower>()
                                .HasOne(m => m.SourceUser)
                                .WithMany(t => t.SourceUserFollowers)
                                .HasForeignKey(m => m.UserId)
                                .OnDelete(DeleteBehavior.Cascade);

                        builder.Entity<UserFollower>()
                                .HasOne(m => m.TargetUser)
                                .WithMany(t => t.TargetUserFollowers)
                                .HasForeignKey(m => m.TargetId)
                                .OnDelete(DeleteBehavior.Cascade);

                        builder.Entity<UserFriend>()
                                .HasOne(m => m.SourceUser)
                                .WithMany(t => t.SourceUserFriends)
                                .HasForeignKey(m => m.SourceId)
                                .OnDelete(DeleteBehavior.Cascade);

                        builder.Entity<UserFriend>()
                                .HasOne(m => m.TargetUser)
                                .WithMany(t => t.TargetUserFriends)
                                .HasForeignKey(m => m.TargetId)
                                .OnDelete(DeleteBehavior.Cascade);

                        builder.Entity<UserImage>()
                                .HasOne(m => m.User)
                                .WithMany(t => t.UserImages)
                                .HasForeignKey(m => m.UserId)
                                .OnDelete(DeleteBehavior.Cascade);

                        builder.Entity<UserImage>()
                                .HasOne(m => m.UserPost)
                                .WithMany(t => t.UserImages)
                                .HasForeignKey(m => m.UserPostId)
                                .OnDelete(DeleteBehavior.Cascade);

                        builder.Entity<UserPost>()
                                .HasOne(m => m.User)
                                .WithMany(t => t.UserPosts)
                                .HasForeignKey(m => m.UserId)
                                .OnDelete(DeleteBehavior.Cascade);

                        builder.Entity<ViewerLike>()
                                .HasOne(m => m.User)
                                .WithMany(t => t.ViewerLikes)
                                .HasForeignKey(m => m.SenderId)
                                .OnDelete(DeleteBehavior.Cascade);

                        builder.Entity<ViewerLike>()
                                .HasOne(m => m.UserPost)
                                .WithMany(t => t.ViewerLikes)
                                .HasForeignKey(m => m.UserPostId)
                                .OnDelete(DeleteBehavior.Cascade);

                        builder.Entity<ViewerComment>()
                                .HasOne(m => m.User)
                                .WithMany(t => t.ViewerComments)
                                .HasForeignKey(m => m.SenderId)
                                .OnDelete(DeleteBehavior.Cascade);

                        builder.Entity<ViewerComment>()
                                .HasOne(m => m.UserPost)
                                .WithMany(t => t.ViewerComments)
                                .HasForeignKey(m => m.UserPostId)
                                .OnDelete(DeleteBehavior.Cascade);

                        builder.Entity<CommentDetail>()
                                .HasOne(m => m.ViewerComment)
                                .WithMany(t => t.CommentDetails)
                                .HasForeignKey(m => m.ViewerCommentId)
                                .OnDelete(DeleteBehavior.Cascade);
                }
        }
}