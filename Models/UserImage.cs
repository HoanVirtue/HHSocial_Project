using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clone_Main_Project_0710.Models
{
    public class UserImage
    {
        [Key]
        public int ImageId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("UserPost")]
        public int UserPostId { get; set; }
        public string? ImageName { get; set; }
        public required byte[] SourceImage { get; set; }
        public bool IsAvatar { get; set; } = false;
        public bool IsCoverImage { get; set; } = false;
    }
}