using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clone_Main_Project_0710.Models
{
    public class CommentDetail
    {
        [Key]
        [ForeignKey("ViewerComment")]
        public Guid ViewerCommentId { get; set; }
        [StringLength(int.MaxValue)]
        public string? Content { get; set; }
        [StringLength(int.MaxValue)]
        public string? ImageComment { get; set; }
        public bool IsDelete { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ViewerComment? ViewerComment { get; set; }
    }
}