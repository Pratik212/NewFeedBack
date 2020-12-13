using System.ComponentModel.DataAnnotations;
using FeedBack.Models;

namespace FeedBack.Dtos
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string Password { get; set; }

        public Roles Role { get; set; }
    }
}