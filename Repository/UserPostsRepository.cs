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
        public UserPostsRepository(SocialContext context)
        {
            _context = context;
            _imageContext = new UserImagesRepository(context);
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

        public Task<UserPost> FindByID(Guid id)
        {
            throw new NotImplementedException();
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

        public async Task<List<PostView>> GetMyPostsView(Guid userId)
        {
            List<PostView> postViews = new List<PostView>();
            List<UserPost> userPosts = await GetMyPosts(userId);
            foreach(UserPost post in userPosts)
            {
                PostView view = new PostView();

                view.UserPost = post;
                if(post.HasImage)
                {
                    List<UserImage> listImage = await _imageContext.GetImagesByPostId(post.UserPostId);
                    view.UserImage = listImage.FirstOrDefault();
                }
                view.ImageAvatar = await _imageContext.GetAvatarByUserId(userId);
                view.Like = await CheckUserLike(userId, post.UserPostId);
                view.Comments = await GetCommentsByPostId(post.UserPostId);
                postViews.Add(view);
            }

            return postViews;
        }

        public async Task<UserLikeView> UserLikePost(ViewerLike model)
        {
            ViewerLike liker = await _context.ViewerLikes.SingleOrDefaultAsync(m => m.UserPostId.Equals(model.UserPostId) && m.SenderId.Equals(model.SenderId));
            int typeLike = -1;
            if(liker == null)
            {
                _context.ViewerLikes.Add(model);
                typeLike = LikeTypeConstant.LIKED;
            }
            else
            {
                if(liker.LikePost)
                {
                    liker.LikePost = false;
                    typeLike = LikeTypeConstant.NOT_LIKE;
                }
                else
                {
                    liker.LikePost = true;
                    typeLike = LikeTypeConstant.LIKED;
                }
                    
                liker.UpdatedAt = DateTime.Now;
                
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
            if(post != null)
            {
                if(typeLike == LikeTypeConstant.LIKED)
                    post.Likes += 1;
                else if(typeLike == LikeTypeConstant.NOT_LIKE)
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
            if(liker != null && liker.LikePost)
            {
                like = true;
            }
            return like;
        }

        public async Task<int> UserCommentPost(ViewerComment commentModel, CommentDetail detail)
        {
            ViewerComment commentator = await _context.ViewerComments.SingleOrDefaultAsync(m => m.UserPostId.Equals(commentModel.UserPostId) && m.SenderId.Equals(commentModel.SenderId));
            if(commentator != null)
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
            int count = await UpdateQuantityComment(commentModel.UserPostId);
            await _context.SaveChangesAsync();

            return count;
        }

        public async Task<int> UpdateQuantityComment(Guid postId)
        {
            UserPost post = await _context.UserPosts.FindAsync(postId);
            int count = 0;
            if(post != null)
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
            foreach(UserPost post in userPosts)
            {
                PostView view = new PostView();

                view.UserPost = post;
                if(post.HasImage)
                {
                    List<UserImage> listImage = await _imageContext.GetImagesByPostId(post.UserPostId);
                    view.UserImage = listImage.FirstOrDefault();
                }
                view.ImageAvatar = await _imageContext.GetAvatarByUserId(post.UserId);
                view.Like = await CheckUserLike(userId, post.UserPostId);
                view.Comments = await GetCommentsByPostId(post.UserPostId);
                postViews.Add(view);
            }

            return postViews;
        } 
    }
}