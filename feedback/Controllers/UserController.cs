using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FeedBack.Dtos;
using FeedBack.Helpers;
using FeedBack.Interfaces;
using FeedBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace FeedBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly Settings _settings;

        public UserController(IUserRepository userRepository , IMapper mapper , IOptions<Settings> settings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _settings = settings.Value;
        }
            
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody]UserDto userDto)
        {
            try
            {
                var users = _mapper.Map<User>(userDto);

                var userData = await _userRepository.UserByEmail(userDto.Email);

                if (userData != null)
                    return BadRequest(new ResponseModel {ErrorCode = "U005", Message = ErrorMessage.U005});
            
                Util.CreatePasswordHash(userDto.Password, out var passwordHash, out var passwordSalt);

                users.PasswordHash = passwordHash;
                users.PasswordSalt = passwordSalt;
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, users.Id.ToString()),
                        new Claim(ClaimTypes.Role, users.Role.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(1)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
            
                await _userRepository.Register(users);
            
                return Ok(new
                {
                    users.Email,
                    users.Role,
                    users.FirstName,
                    users.LastName,
                    Token = tokenString,
                    Expiration = token.ValidTo
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] LoginDto userDto)
        {
            try
            {
                var user = await _userRepository.Login(userDto.Email , userDto.Password);

                if (user == null)
                    return BadRequest(new ResponseModel {ErrorCode = "U004",Message = ErrorMessage.U004});

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_settings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
            
                Log.Information($"{user.Email} Successfully login");
            
                return Ok(new
                {
                    user.Id,
                    user.Role,
                    user.FirstName,
                    user.LastName,
                    user.PhoneNumber,
                    user.Email,
                    Token = tokenString,
                    Expiration = token.ValidTo
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