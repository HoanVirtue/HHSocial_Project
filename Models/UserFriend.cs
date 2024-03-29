using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Clone_Main_Project_0710.Models;

namespace HHSocialNetwork_Project.Models
{
    public class UserFriend
    {
        [Key]
        public Guid UserFriendId { get; set; }
        [ForeignKey("User")]
        public Guid SourceId { get; set; }
        [ForeignKey("User")]
        public Guid TargetId { get; set; }
        public string? TypeFriend { get; set; }
        public bool Status { get; set; }
        public bool IsFriend { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDelete { get; set; } = false;
        public virtual User? SourceUser { get; set; }
        public virtual User? TargetUser { get; set; }
    }
}