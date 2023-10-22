using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Clone_Main_Project_0710.Repository
{
    public class UsersRepository : IRepository<User>
    {
        private SocialContext _context;
        public UsersRepository(SocialContext context)
        {
            _context = context;
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
            User user = await _context.Users.FindAsync(id);

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

        public bool isExistAccount(string? email, string? password)
        {
            var account = _context.Users.
            Where(u => u.Email.Equals(email) && u.Password.Equals(password)).FirstOrDefault();

            return account != null;
        }

        public async Task<User> getUserByAccount(string email, string password)
        {
            User? user = await _context.Users.SingleOrDefaultAsync(p => p.Email.Equals(email) && p.Password.Equals(password));

            return user;
        }

        public async Task<Guid> getIdByEmail(string email, string password)
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
    }
}