using System.Text.Json;
using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.GendericMethod;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Clone_Main_Project_0710.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Clone_Main_Project_0710.Controllers
{
    public class UserPostsController : Controller
    {
        private UserPostsRepository _postContext;
        private UserImagesRepository _imageContext;
        private UsersRepository _userContext;

        public UserPostsController(SocialContext context)
        {
            _postContext = new UserPostsRepository(context);
            _imageContext = new UserImagesRepository(context);
            _userContext = new UsersRepository(context);
        }

        private Guid userIdTest = new Guid("f98d25ef-75b8-4f1e-8e09-9281d33e8f32");
        private string emailTest = "hoan@gmail.com";
        private void SetSession()
        {
            HttpContext.Session.SetString(DataSession.SessionData.USERID_SESS, userIdTest.ToString());
            HttpContext.Session.SetString(DataSession.SessionData.USER_EMAIL_SESS, emailTest);
        }

        public async Task<IActionResult> CreatePost(string Content, bool HasImage, IFormFile ImagePost)
        {
            // SetSession();
            bool isSuccess = true;
            UserPost post = null;
            UserImage userImage = null;
            string errorMsg = "";
            try
            {
                Guid userId = Guid.Parse(Request.Cookies[UsersCookiesConstant.CookieUserId]);
                post = new UserPost()
                {
                    UserPostId = Guid.NewGuid(),
                    UserId = userId,
                    Content = Content,
                    Likes = 0,
                    Comments = 0,
                    HasImage = HasImage,
                    Privacy = "public",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _postContext.Add(post);

                if (HasImage && ImagePost != null && ImagePost.Length > 0)
                {
                    userImage = new UserImage
                    {
                        UserId = userId,
                        UserPostId = post.UserPostId,
                        ImageName = ImagePost.FileName,
                        ImageData = ImageMethod.ConvertImageToString(ImagePost),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    await _imageContext.Add(userImage);
                }
            }
            catch (Exception e)
            {
                isSuccess = false;
                errorMsg = e.Message;
            }

            var data = new
            {
                isSuccess = isSuccess,
                error = errorMsg,
                post = post,
                imagePost = userImage
            };

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string jsonData = JsonConvert.SerializeObject(data, settings);
            return new JsonResult(jsonData);
        }

        [HttpPost]
        public async Task<IActionResult> LikePost(string postId)
        {
            Guid userId = Guid.Parse(Request.Cookies[UsersCookiesConstant.CookieUserId]);
            bool isSuccess = true;
            string errorMsg = "";
            int typeLike = -1;
            int countLike = -1;
            UserLikeView likeView = null;
            List<User> likers = new List<User>();
            if (!string.IsNullOrEmpty(postId))
            {
                Guid idPost = Guid.Parse(postId);

                try
                {
                    ViewerLike model = new ViewerLike()
                    {
                        ViewerLikeId = Guid.NewGuid(),
                        SenderId = userId,
                        UserPostId = idPost,
                        LikePost = true,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    likeView = await _postContext.UserLikePost(model);
                    typeLike = likeView.TypeLike;
                    countLike = likeView.CountLike;
                    likers = await _postContext.GetUserLikes(idPost);
                }
                catch (Exception er)
                {
                    isSuccess = false;
                    errorMsg = er.Message;
                }
            }
            else
            {
                isSuccess = false;
                errorMsg = "trying to log into the system without permission";
            }

            JsonResult data = Json(new
            {
                isSuccess = isSuccess,
                error = errorMsg,
                typeLike = typeLike,
                countLike = countLike,
                likers = likers
            });

            return data;
        }

        [HttpPost]
        public async Task<IActionResult> CommentPost(string UserPostId, string Content, IFormFile ImageComment)
        {
            bool isSuccess = true;
            string errorMsg = "";
            int quantityComment = 0;
            ViewerComment commentModel = null;
            CommentDetail detail = null;
            CommentatorDetail commentator = null;
            List<User> userComments = new List<User>();
            try
            {
                Guid userId = Guid.Parse(Request.Cookies[UsersCookiesConstant.CookieUserId]);
                Guid postId = Guid.Parse(UserPostId);
                string image = null;
                if(ImageComment != null && ImageComment.Length > 0)
                {
                    image = ImageMethod.ConvertImageToString(ImageComment);
                }
                commentModel = new ViewerComment()
                {
                    SenderId = userId,
                    UserPostId = postId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                detail = new CommentDetail()
                {
                    CommentDetailId = Guid.NewGuid(),
                    Content = Content,
                    ImageComment = image,   
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                quantityComment = await _postContext.UserCommentPost(commentModel, detail);

                User user = await _userContext.FindByID(userId);
                UserImage avatar = await _imageContext.GetAvatarByUserId(userId);
                commentator = new CommentatorDetail()
                {
                    CommentDetailId = detail.CommentDetailId,
                    Content = Content,
                    ImageContent = image,
                    CreatedAt = (DateTime)detail.CreatedAt,
                    UpdatedAt = (DateTime)detail.UpdatedAt,
                    SenderId = commentModel.SenderId,
                    UserName = user.UserName,
                    AvatarImage = avatar.ImageData
                };

                userComments = await _postContext.GetUserComments(postId);

            } catch(Exception e) {
                isSuccess = false;
                errorMsg = e.Message;
            }

            JsonResult data = Json(new {
                isSuccess = isSuccess,
                errorMsg = errorMsg,
                countComment = quantityComment,
                commentator = commentator,
                userComments = userComments
            });

            return data;
        }
    }
}