using FeedBack.Models;

namespace FeedBack.Dtos
{
    /// <summary>
    /// FeedBackDto
    /// </summary>
    public class FeedBackDto
    {
        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// FeedbackType
        /// </summary>
        public FeedbackType FeedbackType { get; set; }
    }
}