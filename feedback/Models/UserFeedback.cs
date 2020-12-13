using System.ComponentModel.DataAnnotations;

namespace FeedBack.Models
{
    public class UserFeedback
    {
        public long Id { get; set; }

        public FeedbackType FeedbackType { get; set; }
        
        public string Description { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
    }
    public enum FeedbackType
    {
        Comments = 1,
        Suggestions = 2,
        Questions = 3,
    }
}