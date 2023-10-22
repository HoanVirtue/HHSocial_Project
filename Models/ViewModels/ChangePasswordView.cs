using System.ComponentModel.DataAnnotations;

namespace Clone_Main_Project_0710.Models.ViewModels
{
    public class ChangePasswordView
    {
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}