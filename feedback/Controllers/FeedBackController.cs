using System;
using System.Threading.Tasks;
using FeedBack.Dtos;
using FeedBack.Interfaces;
using FeedBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedBack.Controllers
{
    /// <summary>
    /// FeedBackController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : Controller
    {
        private readonly IUserFeedBackRepository _userFeedBackRepository;
        private readonly FeedBackContext _context;

        public FeedBackController(IUserFeedBackRepository userFeedBackRepository , FeedBackContext context)
        {
            _userFeedBackRepository = userFeedBackRepository;
            _context = context;
        }
    
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddFeedBack([FromBody]FeedBackDto feedBackDto)
        {
            try
            {
                var userFeedback = new UserFeedback()
                {
                    FeedbackType = feedBackDto.FeedbackType,
                    Description = feedBackDto.Description,
                    FirstName = feedBackDto.FirstName,
                    LastName = feedBackDto.LastName,
                    Email = feedBackDto.Email
                };
                
                await _userFeedBackRepository.AddFeedBack(userFeedback);

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    userFeedback.Id,
                    userFeedback.FeedbackType,
                    userFeedback.Description,
                    userFeedback.FirstName,
                    userFeedback.LastName,
                    userFeedback.Email
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}