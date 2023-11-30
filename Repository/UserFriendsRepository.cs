using BTL.Models.ViewModels;
using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Repository;
using HHSocialNetwork_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Clone_Main_Project_0710.Repository
{
    public class UserFriendsRepository : IRepository<UserFriend>
    {
        private SocialContext _context;
        private UsersRepository _userContext;
        private UserImagesRepository _imageContext;

        public UserFriendsRepository(SocialContext context)
        {
            _context = context;
            _userContext = new UsersRepository(context);
            _imageContext = new UserImagesRepository(context);
        }
        public Task Add(UserFriend entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserFriend> FindByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserFriend>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserFriend>> GetAll(Guid targetId)
        {
            List<UserFriend> listUserFriend = await _context.UserFriends.Where(m => m.TargetId.Equals(targetId) && m.IsFriend == false && m.IsDelete == false)
                                                                        .Include(m => m.SourceUser)
                                                                        .Include(m => m.TargetUser)
                                                                        .ToListAsync();

            return listUserFriend;
        }

        public Task Update(UserFriend entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FriendRequestView>> GetListFriend(Guid userId)
        {
            List<FriendRequestView> listFriend = new List<FriendRequestView>();

            List<UserFriend> userFriends = await GetAll(userId);
            foreach (UserFriend u in userFriends)
            {
                UserImage image = await _imageContext.GetAvatarByUserId(u.SourceId);

                FriendRequestView friend = new FriendRequestView()
                {
                    UserFriend = u,
                    UserImage = image
                };

                listFriend.Add(friend);
            }

            return listFriend;
        }

        public async Task ConfirmRequestFriend(Guid sourceId, Guid targetId, string type)
        {
            UserFriend userFriend = await _context.UserFriends.SingleOrDefaultAsync(m => m.SourceId.Equals(sourceId) && m.TargetId.Equals(targetId));
            if (userFriend != null)
            {
                if (type.Equals(HandleFriendTypeConstant.TYPE_ACCEPT_CONFIRM))
                {
                    userFriend.IsFriend = true;
                    userFriend.UpdatedAt = DateTime.Now;
                }
                else if (type.Equals(HandleFriendTypeConstant.TYPE_DELETE_CONFIRM))
                {
                    userFriend.IsDelete = true;
                    userFriend.UpdatedAt = DateTime.Now;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task AddOrCancelFriend(Guid sourceId, Guid targetId, string type)
        {
            UserFriend userFriend = _context.UserFriends.SingleOrDefault(m => m.SourceId.Equals(sourceId) && m.TargetId.Equals(targetId));
            if (userFriend != null)
            {
                if (type.Equals(HandleFriendTypeConstant.TYPE_CANCEL_INVATION))
                {
                    userFriend.IsDelete = true;
                    userFriend.UpdatedAt = DateTime.Now;
                }
                else if (type.Equals(HandleFriendTypeConstant.TYPE_ADD_FRIEND))
                {
                    userFriend.IsDelete = false;
                    userFriend.CreatedAt = DateTime.Now;
                    userFriend.UpdatedAt = DateTime.Now;
                }
            }
            else
            {
                if (type == HandleFriendTypeConstant.TYPE_ADD_FRIEND)
                {
                    userFriend = new UserFriend()
                    {
                        UserFriendId = Guid.NewGuid(),
                        SourceId = sourceId,
                        TargetId = targetId,
                        Status = false,
                        IsFriend = false,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        IsDelete = false
                    };
                    await _context.UserFriends.AddAsync(userFriend);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<UserFriend> GetDataByUser(Guid sourceId, Guid targetId)
        {
            return await _context.UserFriends.SingleOrDefaultAsync(m => m.SourceId.Equals(sourceId) && m.TargetId.Equals(targetId));
        }

        public async Task<List<FriendRequestView>> GetFriendsByTargetIdAsync(Guid userId)
        {
            List<FriendRequestView> friendRequestViews = new List<FriendRequestView>();
            List<UserFriend> listUserFriend = await _context.UserFriends.Where(m => (m.TargetId.Equals(userId) || m.SourceId.Equals(userId)) && m.IsFriend && m.IsDelete == false)
                                                                        .Include(m => m.SourceUser)
                                                                        .Include(m => m.TargetUser)
                                                                        .ToListAsync();
            if (listUserFriend.Count() > 0)
            {
                foreach (UserFriend u in listUserFriend)
                {
                    UserImage image = await _imageContext.GetAvatarByUserId(u.SourceId);

                    FriendRequestView friend = new FriendRequestView()
                    {
                        UserFriend = u,
                        UserImage = image
                    };

                    friendRequestViews.Add(friend);
                }
            }

            return friendRequestViews;
        }
    }
}