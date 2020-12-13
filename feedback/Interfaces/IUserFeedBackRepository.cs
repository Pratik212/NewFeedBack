using System.Threading.Tasks;
using FeedBack.Models;

namespace FeedBack.Interfaces
{
    public interface IUserFeedBackRepository
    {
        Task<UserFeedback> AddFeedBack(UserFeedback userFeedback);
    }
}