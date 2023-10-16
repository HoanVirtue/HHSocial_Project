using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clone_Main_Project_0710.Models
{
    public class UserPost
    {
        [Key]
        public Guid UserPostId { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [StringLength(3000)]
        public string? Content { get; set; }
        [StringLength(255)]
        public string? ImagePost { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}