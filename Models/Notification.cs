using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clone_Main_Project_0710.Models
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }
        [ForeignKey("User")]
        public Guid? SourceId { get; set; }
        [ForeignKey("User")]
        public Guid? TargetId { get; set; }
        [ForeignKey("UserPost")]
        public Guid? UserPostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Read { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual User? SourceUser { get; set; }
        public virtual User? TargetUser { get; set; }
        public virtual UserPost? UserPost { get; set; }
    }
}