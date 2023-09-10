using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHSocialNetwork_Project.Models
{
    public class ViewerFeed_Like
    {
        [Key]
        public int ViewerId { get; set; }
        [ForeignKey("User")]
        public int SenderId { get; set; }
        [ForeignKey("UserPost")]
        public int UserPostId { get; set; }
        public bool LikePost { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}