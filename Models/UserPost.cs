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
        public int Likes { get; set; }
        public int Comments { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<UserImage>? UserImages { get; set; }
        public virtual ICollection<ViewerLike>? ViewerLikes { get; set; }
        public virtual ICollection<ViewerComment>? ViewerComments { get; set; }
    }
}