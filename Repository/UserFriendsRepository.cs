using BTL.Models.ViewModels;
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

        public UserFriendsRepository (SocialContext context)
        {
            _context=context;
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
            
            List<UserFriend> listUserFriend = await _context.UserFriends.Where(m => m.TargetId.Equals(targetId) && m.IsFriend == false).ToListAsync();
            foreach(UserFriend u in listUserFriend) {
                u.SourceUser = await _userContext.FindByID(u.SourceId);
                u.TargetUser = await _userContext.FindByID(u.TargetId);
            }
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
            foreach(UserFriend u in userFriends)
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
            UserFriend userFriend = await _context.UserFriends.SingleOrDefaultAsync(m => m.SourceId == sourceId && m.TargetId == targetId);
            if (userFriend != null)
            {
                if (type.Equals("accept"))
                {
                    userFriend.IsFriend = true;
                    userFriend.UpdatedAt = DateTime.Now;
                }
                else if (type.Equals("delete"))
                {
                    userFriend.IsDelete = true;
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}