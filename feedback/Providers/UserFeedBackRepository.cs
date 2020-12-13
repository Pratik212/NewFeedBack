using System.Threading.Tasks;
using FeedBack.Interfaces;
using FeedBack.Models;

namespace FeedBack.Providers
{
    public class UserFeedBackRepository:IUserFeedBackRepository
    {
        private readonly FeedBackContext _context;
        
        public  UserFeedBackRepository(FeedBackContext context)
        {
            _context = context;   
        }

        public async Task<UserFeedback> AddFeedBack(UserFeedback userFeedback)
        {
            var feedBack = await _context.UserFeedbacks.AddAsync(userFeedback);
            await _context.SaveChangesAsync();
            return feedBack.Entity;
        }
    }
}