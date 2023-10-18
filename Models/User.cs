using System.ComponentModel.DataAnnotations;

namespace Clone_Main_Project_0710.Models
{
    public class User
    {
        //tungcommand
        [Key]
        public Guid UserId { get; set; }
        [Required]
        [StringLength(150)]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(150)]
        public string? LastName { get; set; }
        [StringLength(150)]
        public string? UserName { get; set; }
        [StringLength(15)]
        public string? NumberPhone { get; set; }
        public DateTime? Birthday { get; set; }
        public string? GenderName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [StringLength(255)]
        public string? Password { get; set; }
        public DateTime? RegisterAt { get; set; }
        public DateTime? LastLogin { get; set; }
        [StringLength(3000)]
        public string? Intro { get; set; }
        [StringLength(30000)]
        public string? Profile { get; set; }
        public bool? role { get; set; } = false;
    }
}