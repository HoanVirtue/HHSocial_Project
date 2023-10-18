using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clone_Main_Project_0710.Models
{
    public class UserComment
    {
        [Key]
        public Guid CommentId { get; set; }
        [ForeignKey("ViewerFeed_Like")]
        public Guid ViewerId { get; set; }
        public string? Content { get; set; }
        public string? ImageComment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}