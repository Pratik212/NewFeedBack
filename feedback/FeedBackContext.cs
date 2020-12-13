using FeedBack.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedBack
{
    public class FeedBackContext :DbContext
    {
        public FeedBackContext(DbContextOptions<FeedBackContext> options) : base(options)
        {
        }
        
        public virtual DbSet<UserFeedback>UserFeedbacks { get; set; }
        
        public virtual  DbSet<User>Users { get; set; }
    }
}