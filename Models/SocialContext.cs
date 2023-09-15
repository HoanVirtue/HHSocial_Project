using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HHSocialNetwork_Project.Models
{
    public class SocialContext : IdentityDbContext
    {
        public SocialContext(DbContextOptions<SocialContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<UserFollower> userFollowers { get; set; }
        public DbSet<UserPost> userPosts { get; set; }
        public DbSet<ViewerFeed_Like> viewerFeed_Likes { get; set; }
    }
}