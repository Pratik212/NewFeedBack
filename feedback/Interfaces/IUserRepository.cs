using System;
using System.Threading.Tasks;
using FeedBack.Models;

namespace FeedBack.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Register(User user);

        Task<User> UserByEmail(string email);

        Task<User> Login(string email, string password);
        Task<User> GetUserById(long userId);
    }
}