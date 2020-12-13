using System.ComponentModel;

namespace FeedBack.Models
{
    public class User
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        // If Admin = 1 , User = 2
        public Roles Role { get; set; }

        public byte[] PasswordHash { get; set; }
        
        public byte[] PasswordSalt { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
    public enum Roles
    {
        [Description("ADMIN")] Admin = 1,
        
        [Description("USER")] User = 2,
    }
}