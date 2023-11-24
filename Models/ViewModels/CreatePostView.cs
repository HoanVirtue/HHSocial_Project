namespace Clone_Main_Project_0710.Models.ViewModels
{
    public class CreatePostView
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public UserPost? Post { get; set; }
        public UserImage? ImagePost { get; set; }
    }
}