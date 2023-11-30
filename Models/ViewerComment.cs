using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clone_Main_Project_0710.Models
{
    public class ViewerComment
    {
        public ViewerComment()
        {
            CommentDetails = new HashSet<CommentDetail>();
        }
        [Key]
        public Guid ViewerCommentId { get; set; }
        [ForeignKey("User")]
        public Guid SenderId { get; set; }
        [ForeignKey("UserPost")]
        public Guid UserPostId { get; set; }
        public int CommentsCount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual User? User { get; set; }
        public virtual UserPost? UserPost { get; set; }
        public virtual ICollection<CommentDetail>? CommentDetails { get; set; }
    }
}