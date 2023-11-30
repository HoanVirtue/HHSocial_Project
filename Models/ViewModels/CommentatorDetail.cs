namespace Clone_Main_Project_0710.Models.ViewModels
{
    public class CommentatorDetail
    {
        public Guid CommentDetailId { get; set; }
        public Guid ViewerCommentId { get; set; }
        public string Content { get; set; }
        public string ImageContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid SenderId { get; set; }
        public string UserName { get; set; }
        public string AvatarImage { get; set; }
    }
}