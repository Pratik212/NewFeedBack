using System.Threading.Tasks;
using FeedBack.Helpers;
using FeedBack.Interfaces;
using FeedBack.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedBack.Providers
{
    public class UserRepository : IUserRepository
    {
        private readonly FeedBackContext _context;

        public UserRepository(FeedBackContext context)
        {
            _context = context;
        }
        
        public async Task<User> Register(User user)
        {
            var userRegister = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return userRegister.Entity;
        }

        public async Task<User>UserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await UserByEmail(email);

            if (user == null)
                return null;

            if (user.PasswordHash == null && user.PasswordSalt == null)
                return null;
            
            return !Util.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? null : user;
        }

        public async Task<User> GetUserById(long userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}