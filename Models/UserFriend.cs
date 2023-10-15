using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clone_Main_Project_0710.Models
{
    public class UserFriend
    {
        [Key]
        public int UserFriendId { get; set; }
        [ForeignKey("User")]
        public int SourceId { get; set; }
        [ForeignKey("User")]
        public int TargetId { get; set; }
        public string? TypeFriend { get; set; }
        public bool Status { get; set; }
        public bool IsFriend { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}