using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHSocialNetwork_Project.Models
{
    public class UserComment
    {
        [Key]
        public int CommentId { get; set; }
        [ForeignKey("ViewerFeed_Like")]
        public int ViewerId { get; set; }
        public string? Content { get; set; }
        public string? ImageComment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}