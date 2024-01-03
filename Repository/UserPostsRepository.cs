using System.Data.Common;
using System.Net.Mime;
using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Clone_Main_Project_0710.Repository
{
    public class UserPostsRepository : IRepository<UserPost>
    {
        public SocialContext _context;
        public UserImagesRepository _imageContext;
        public NotifitcationRepository _notifiContext;
        public UsersRepository _userContext;
        public UserPostsRepository(SocialContext context)
        {
            _context = context;
            _imageContext = new UserImagesRepository(context);
            _notifiContext = new NotifitcationRepository(context);
            _userContext = new UsersRepository(context);
        }
        public async Task Add(UserPost entity)
        {
            await _context.UserPosts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserPost> FindByID(Guid id)
        {
            return await _context.UserPosts.Include(m => m.User).SingleOrDefaultAsync(m => m.UserPostId.Equals(id));
        }

        public Task<List<UserPost>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(UserPost entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserPost>> GetMyPosts(Guid userId)
        {
            List<UserPost> listPost = await _context.UserPosts.Include(m => m.User)
                                                                .Where(m => m.UserId.Equals(userId))
                                                                .OrderByDescending(m => m.UpdatedAt).ToListAsync();

            return listPost;
        }

        public async Task<List<PostView>> GetMyPostsView(Guid userCurrentId, Guid userId)
        {
            List<PostView> postViews = new List<PostView>();
            List<UserPost> userPosts = await GetMyPosts(userId);
            foreach (UserPost post in userPosts)
            {
                PostView view = new PostView();

                view.UserPost = post;
                if (post.HasImage)
                {
                    List<UserImage> listImage = await _imageContext.GetImagesByPostId(post.UserPostId);
                    view.UserImage = listImage.FirstOrDefault();
                }
                view.ImageAvatar = await _imageContext.GetAvatarByUserId(userId);
                view.Like = await CheckUserLike(userCurrentId, post.UserPostId);
                view.ViewerLikes = await GetUserLikes(post.UserPostId);
                view.Comments = await GetCommentsByPostId(post.UserPostId);
                view.ViewerComments = await GetUserComments(post.UserPostId);
                postViews.Add(view);
            }

            return postViews;
        }

        public async Task<UserLikeView> UserLikePost(ViewerLike model)
        {
            ViewerLike liker = await _context.ViewerLikes.SingleOrDefaultAsync(m => m.UserPostId.Equals(model.UserPostId) && m.SenderId.Equals(model.SenderId));
            int typeLike = -1;
            bool createNotifi = true;
            if (liker == null)
            {
                _context.ViewerLikes.Add(model);
                typeLike = LikeTypeConstant.LIKED;
            }
            else
            {
                if (liker.LikePost)
                {
                    liker.LikePost = false;
                    typeLike = LikeTypeConstant.NOT_LIKE;
                    createNotifi = false;
                }
                else
                {
                    liker.LikePost = true;
                    typeLike = LikeTypeConstant.LIKED;
                }

                liker.UpdatedAt = DateTime.Now;
            }

            // create notification
            if (createNotifi)
            {
                User user = await _userContext.FindByID(model.SenderId);
                UserPost post = await FindByID(model.UserPostId);
                if (!model.SenderId.Equals(post.UserId))
                {
                    string formatContent = FormatContentPost(post.Content);
                    Notification notifi = new Notification()
                    {
                        NotificationId = Guid.NewGuid(),
                        SourceId = model.SenderId,
                        TargetId = post.UserId,
                        UserPostId = post.UserPostId,
                        Content = string.Format("<b>{0}</b> thích bài viết của bạn.: {1}", user.UserName, formatContent),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    await _notifiContext.Add(notifi);
                }
            }

            int countLike = await UpdateQuantityLike(model.UserPostId, typeLike);
            await _context.SaveChangesAsync();

            UserLikeView view = new UserLikeView()
            {
                TypeLike = typeLike,
                CountLike = countLike
            };
            return view;
        }

        public async Task<int> UpdateQuantityLike(Guid postId, int typeLike)
        {
            int countLike = 0;
            UserPost post = await _context.UserPosts.FindAsync(postId);
            if (post != null)
            {
                if (typeLike == LikeTypeConstant.LIKED)
                    post.Likes += 1;
                else if (typeLike == LikeTypeConstant.NOT_LIKE)
                    post.Likes -= 1;
                countLike = post.Likes;
            }

            await _context.SaveChangesAsync();
            return countLike;
        }

        public async Task<bool> CheckUserLike(Guid userId, Guid postId)
        {
            bool like = false;
            ViewerLike liker = await _context.ViewerLikes.SingleOrDefaultAsync(m => m.SenderId.Equals(userId) && m.UserPostId.Equals(postId));
            if (liker != null && liker.LikePost)
            {
                like = true;
            }
            return like;
        }

        public async Task<int> UserCommentPost(ViewerComment commentModel, CommentDetail detail)
        {
            ViewerComment commentator = await _context.ViewerComments.SingleOrDefaultAsync(m => m.UserPostId.Equals(commentModel.UserPostId) && m.SenderId.Equals(commentModel.SenderId));
            bool createNotifi = true;
            if (commentator != null)
            {
                commentator.CommentsCount += 1;
                detail.ViewerCommentId = commentator.ViewerCommentId;
                await _context.CommentDetails.AddAsync(detail);
            }
            else
            {
                commentModel.ViewerCommentId = Guid.NewGuid();
                commentModel.CommentsCount = 1;
                await _context.ViewerComments.AddAsync(commentModel);
                detail.ViewerCommentId = commentModel.ViewerCommentId;
                await _context.CommentDetails.AddAsync(detail);
            }

            //create notifi
            UserPost post = await FindByID(commentModel.UserPostId);
            User user = await _userContext.FindByID(commentModel.SenderId);
            if (!post.UserId.Equals(user.UserId))
            {
                Notification notifi = new Notification()
                {
                    NotificationId = Guid.NewGuid(),
                    SourceId = commentModel.SenderId,
                    TargetId = post.UserId,
                    UserPostId = post.UserPostId,
                    Content = string.Format("<b>{0}</b> đã bình luận về bài viết của bạn.", user.UserName),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await _notifiContext.Add(notifi);
            }


            int count = await UpdateQuantityComment(commentModel.UserPostId);
            await _context.SaveChangesAsync();

            return count;
        }

        public async Task<int> UpdateQuantityComment(Guid postId)
        {
            UserPost post = await _context.UserPosts.FindAsync(postId);
            int count = 0;
            if (post != null)
            {
                post.Comments += 1;
                count = post.Comments;
            }
            await _context.SaveChangesAsync();

            return count;
        }

        public async Task<List<CommentatorDetail>> GetCommentsByPostId(Guid postId)
        {
            var listComment = await (from cd in _context.CommentDetails
                                     join vc in _context.ViewerComments
                                     on cd.ViewerCommentId equals vc.ViewerCommentId
                                     join ui in _context.UserImages
                                     on vc.SenderId equals ui.UserId
                                     where vc.UserPostId == postId && ui.IsAvatar == true
                                     select new CommentatorDetail()
                                     {
                                         CommentDetailId = cd.CommentDetailId,
                                         ViewerCommentId = cd.ViewerCommentId,
                                         Content = cd.Content,
                                         ImageContent = cd.ImageComment,
                                         CreatedAt = (DateTime)cd.CreatedAt,
                                         UpdatedAt = (DateTime)cd.UpdatedAt,
                                         SenderId = vc.SenderId,
                                         UserName = vc.User.UserName,
                                         AvatarImage = ui.ImageData
                                     })
                            .OrderByDescending(cd => cd.UpdatedAt)
                            .ToListAsync();

            return listComment;
        }

        public async Task<List<UserPost>> GetPosts()
        {
            List<UserPost> listPost = await _context.UserPosts.Include(m => m.User)
                                                                .Include(m => m.UserImages)
                                                                .OrderByDescending(m => m.UpdatedAt).ToListAsync();

            return listPost;
        }

        public async Task<List<PostView>> GetAllPosts(Guid userId)
        {
            List<PostView> postViews = new List<PostView>();
            List<UserPost> userPosts = await GetPosts();
            foreach (UserPost post in userPosts)
            {
                PostView view = new PostView();

                view.UserPost = post;
                if (post.HasImage)
                {
                    List<UserImage> listImage = await _imageContext.GetImagesByPostId(post.UserPostId);
                    view.UserImage = listImage.FirstOrDefault();
                }
                view.ImageAvatar = await _imageContext.GetAvatarByUserId(post.UserId);
                view.Like = await CheckUserLike(userId, post.UserPostId);
                view.ViewerLikes = await GetUserLikes(post.UserPostId);
                view.Comments = await GetCommentsByPostId(post.UserPostId);
                view.ViewerComments = await GetUserComments(post.UserPostId);
                postViews.Add(view);
            }

            return postViews;
        }

        public async Task<List<User>> GetUserLikes(Guid postId)
        {
            List<User> users = new List<User>();
            var likers = await _context.ViewerLikes.Where(m => m.UserPostId.Equals(postId) && m.LikePost)
                                                                .Include(m => m.User)
                                                                .Select(m => new
                                                                {
                                                                    m.User.UserId,
                                                                    m.User.UserName
                                                                })
                                                                .Distinct()
                                                                .ToListAsync();
            foreach (var data in likers)
            {
                users.Add(new User()
                {
                    UserId = data.UserId,
                    UserName = data.UserName
                });
            }
            return users;
        }

        public async Task<List<User>> GetUserComments(Guid postId)
        {
            List<User> users = new List<User>();
            var comments = await _context.ViewerComments.Where(m => m.UserPostId.Equals(postId))
                                                                    .Include(m => m.User)
                                                                    .Select(m => new
                                                                    {
                                                                        m.User.UserId,
                                                                        m.User.UserName
                                                                    })
                                                                    .Distinct()
                                                                    .ToListAsync();
            foreach (var data in comments)
            {
                users.Add(new User()
                {
                    UserId = data.UserId,
                    UserName = data.UserName
                });
            }
            return users;
        }

        public string FormatContentPost(string content)
        {
            string[] splitContent = content.Split(' ');
            string formatContent = "";
            if (splitContent.Length > 10)
            {
                formatContent = String.Join(" ", splitContent.Take(10)) + "...";
            }
            else
                formatContent = content;
            return formatContent;
        }

        public async Task<PostView> GetPostDetailAsync(Guid userId, Guid postId)
        {
            PostView view = new PostView();
            UserPost post = await FindByID(postId);
            if(post != null) {
                view.UserPost = post;
                if (post.HasImage)
                {
                    List<UserImage> listImage = await _imageContext.GetImagesByPostId(post.UserPostId);
                    view.UserImage = listImage.FirstOrDefault();
                }
                view.ImageAvatar = await _imageContext.GetAvatarByUserId(userId);
                view.Like = await CheckUserLike(userId, postId);
                view.ViewerLikes = await GetUserLikes(postId);
                view.Comments = await GetCommentsByPostId(postId);
                view.ViewerComments = await GetUserComments(postId);
            }
            
            return view;
        }
    }
}