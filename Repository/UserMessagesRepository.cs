using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Clone_Main_Project_0710.Repository
{
    public class UserMessagesRepository : IRepository<UserMessage>
    {
        private SocialContext _context;
        private UsersRepository _userContext;
        public UserMessagesRepository(SocialContext context)
        {
            _context = context;
            _userContext = new UsersRepository(context);
        }
        public Task Add(UserMessage entity)
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

        public Task<UserMessage> FindByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserMessage>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(UserMessage entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserMessage>> GetAll(Guid userIdCurrent)
        {
            List<UserMessage> listUser = await _context.UserMessages
                                                        .Where(u => u.SourceId.Equals(userIdCurrent) || u.TargetId.Equals(userIdCurrent))
                                                        .Include(u => u.SourceUser)
                                                        .Include(u => u.TargetUser)
                                                        .OrderByDescending(u => u.CreatedAt)
                                                        .ToListAsync();
            return listUser;
        }

        public async Task<List<UserMessage_FriendView>> GetAllFilterFriend(Guid userId, string? keySearch)
        {
            List<UserMessage> listUser = await GetAll(userId);
            List<UserMessage_FriendView> listUserFriend = new List<UserMessage_FriendView>();
            foreach(UserMessage mess in listUser)
            {
                Guid userFriendId = userId.Equals(mess.SourceId) ? mess.TargetId : mess.SourceId;
                int typeId = userId.Equals(mess.SourceId) ? UserIdConstant.TARGETID_USER : UserIdConstant.SOURCEID_USER;
                UserMessage_FriendView userMessage = listUserFriend.SingleOrDefault(u => u.UserMessage.SourceId.Equals(userFriendId) || u.UserMessage.TargetId.Equals(userFriendId));
                if(userMessage == null)
                {
                    UserMessage messContain = null;
                    if(!string.IsNullOrEmpty(keySearch))
                    {
                        if(typeId == UserIdConstant.TARGETID_USER)
                            messContain = mess.TargetUser.UserName.Contains(keySearch) ? mess : null;
                        else
                            messContain = mess.SourceUser.UserName.Contains(keySearch) ? mess : null;
                        listUserFriend.Add(new UserMessage_FriendView()
                        {
                            UserMessage = messContain
                        });
                    }
                    else
                    {
                        User friend = mess.SourceId.Equals(userFriendId) ? await _userContext.FindByID(mess.SourceId) : await _userContext.FindByID(mess.TargetId);
                        userMessage = new UserMessage_FriendView()
                        {
                            UserMessage = mess,
                            UserFriend = friend
                        };
                        listUserFriend.Add(userMessage);
                    }
                        
                }
            }
            return listUserFriend;
        }
    }
}