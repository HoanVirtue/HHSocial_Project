using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHSocialNetwork_Project.Models
{
    public class UserFollower
    {
        [Key]
        public int FollowerId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("User")]
        public int TargetId { get; set; }
        public string? TypeFlower { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}