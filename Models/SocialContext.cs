using System.Reflection.Metadata;
using Clone_Main_Project_0710.Models;
using HHSocialNetwork_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clone_Main_Project_0710.Models
{
    public class SocialContext : IdentityDbContext
    {
        public SocialContext(DbContextOptions<SocialContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }
        public DbSet<ViewerFeed_Like> ViewerFeed_Likes { get; set; }
        public DbSet<UserFriend> UserFriends {get; set;}
        public DbSet<UserImage> UserImages {get; set;}
        
    }
}