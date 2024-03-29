using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Clone_Main_Project_0710.Repository
{
    public class UsersRepository : IRepository<User>
    {
        private SocialContext _context ;
        private UserImagesRepository _imageContext;
        public UsersRepository(SocialContext context)
        {
            _context = context;
            _imageContext = new UserImagesRepository(context);
        }

        public async Task Add(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }

        public bool ExistByEmail(string email)
        {
            User? user = _context.Users.SingleOrDefault(p => p.Email.Equals(email));

            return user != null;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByID(Guid id)
        {
            User user = await _context.Users.Include(m=>m.UserImages).SingleOrDefaultAsync(m => m.UserId.Equals(id));

            return user;
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Update(User entity)
        {
            User user = await _context.Users.SingleOrDefaultAsync(m => m.UserId == entity.UserId);
            if (user != null)
            {
                user.FirstName = entity.FirstName;
                user.LastName = entity.LastName;
                user.UserName = entity.UserName;
                user.NumberPhone = entity.NumberPhone;
                user.Birthday = entity.Birthday;
                user.GenderName = entity.GenderName;
                user.Intro = entity.Intro;
                user.Profile = entity.Profile;

                await _context.SaveChangesAsync();
            }
        }

        public bool IsExistAccount(string? email, string? password)
        {
            var account = _context.Users.
            Where(u => u.Email.Equals(email) && u.Password.Equals(password)).FirstOrDefault();

            return account != null;
        }

        public async Task<User> GetUserByAccount(string email, string password)
        {
            User? user = await _context.Users.SingleOrDefaultAsync(p => p.Email.Equals(email) && p.Password.Equals(password));

            return user;
        }

        public async Task<Guid> GetIdByEmail(string email, string password)
        {
            Guid userId = (await _context.Users.SingleOrDefaultAsync(p => p.Email.Equals(email) && p.Password.Equals(password))).UserId;

            return userId;
        }

        public async Task ChangePassword(ChangePasswordView model)
        {
            User user = await _context.Users.FindAsync(model.UserId);
            if(user != null)
            {
                user.Password = model.NewPassword;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetUsers_Friends_Avatar_ByKeySearch(string keySearch)
        {
            List<User> listUser = await _context.Users.Include(u => u.SourceUserFriends)
                                                        .Include(u => u.TargetUserFriends)
                                                        .Include(i => i.UserImages)
                                                        .Where(m => m.UserName.Contains(keySearch))
                                                        .ToListAsync();
            
            return listUser;
        }

        public async Task<List<PersonView>> GetDataByName(string name)
        {
            List<PersonView> list = new List<PersonView>();
            List<UserPost> users = await _context.UserPosts.Where(m => m.Content.Contains(name))
                                                .Include(m => m.User)
                                                .ToListAsync();
            foreach(UserPost post in users)
            {
                PersonView view = new PersonView();
                view.User = post.User;
                view.ImageAvatar = await _imageContext.GetAvatarByUserId(post.UserId);
                list.Add(view);
            }

            return list;
        }

        public async Task<bool> Disable_Enable(Guid userId)
        {
            tblDisable tbl = await _context.tblDisables.FindAsync(userId);
            bool disable = false;
            if(tbl != null)
            {
                tbl.Disable = !tbl.Disable;
                tbl.LogTime = DateTime.Now;
                disable = tbl.Disable;
            }
            else
            {
                tbl = new tblDisable()
                {
                    UserId = userId,
                    Disable = false,
                    LogTime = DateTime.Now 
                };
                _context.tblDisables.Add(tbl);
            }

            _context.SaveChanges();

            return disable;
        }
    }
}