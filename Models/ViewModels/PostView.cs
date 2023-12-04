namespace Clone_Main_Project_0710.Models.ViewModels
{
    public class PostView
    {
        public UserPost UserPost { get; set; }
        public UserImage? UserImage { get; set; }
        public UserImage ImageAvatar { get; set; }
        public bool Like { get; set; }
        public List<User> ViewerLikes { get; set; }
        public List<CommentatorDetail>? Comments { get; set; }
        public List<User> ViewerComments { get; set; }
    }
}