using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clone_Main_Project_0710.Models
{
    public class UserImage
    {
        [Key]
        public Guid ImageId { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [ForeignKey("UserPost")]
        public Guid UserPostId { get; set; }
        public string? ImageName { get; set; }
        [StringLength(int.MaxValue)]
        public string ImageData { get; set; }
        [NotMapped]
        public IFormFile SourceImage { get; set; }
        public bool IsAvatar { get; set; } = false;
        public bool IsCoverImage { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}