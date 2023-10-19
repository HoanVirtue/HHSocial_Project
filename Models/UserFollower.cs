using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clone_Main_Project_0710.Models
{
    public class UserFollower
    {
        [Key]
        public Guid FollowerId { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [ForeignKey("User")]
        public Guid TargetId { get; set; }
        public string? TypeFlower { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual User? SourceUser { get; set; }
        public virtual User? TargetUser { get; set; }
    }
}