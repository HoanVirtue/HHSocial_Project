using System.ComponentModel.DataAnnotations.Schema;

namespace Clone_Main_Project_0710.Models
{
    public class UserMessage
    {
        public Guid UserMessageId { get; set; }
        [ForeignKey("User")]
        public Guid SourceId { get; set; }
        [ForeignKey("User")]
        public Guid TargetId { get; set; }
        public string? Message { get; set; }
        public bool HasImage { get; set; }
        public bool Read { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual User? SourceUser { get; set; }
        public virtual User? TargetUser { get; set; }
        
    }
}