using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clone_Main_Project_0710.Models
{
    public class ViewerFeed_Like
    {
        [Key]
        public Guid ViewerId { get; set; }
        [ForeignKey("User")]
        public Guid SenderId { get; set; }
        [ForeignKey("UserPost")]
        public Guid UserPostId { get; set; }
        public bool LikePost { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual User? User { get; set; }
        public virtual UserPost? UserPost { get; set; }
        public virtual ICollection<UserComment>? UserComments { get; set; }
    }
}