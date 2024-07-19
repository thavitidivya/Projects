using AuthenticationService;
using AuthenticationService.Models;
using AuthenticationService.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Context;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IJwtService _jwtService;

        public 
            AuthenticationController(UserContext context,IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }
        private string GenerateJwtToken(User user)
        {
            return _jwtService.GenerateToken(user.Username);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (userDto == null || string.IsNullOrWhiteSpace(userDto.Username) || string.IsNullOrWhiteSpace(userDto.Password))
            {
                return BadRequest(new { Message = "Invalid user data!" });
            }

            var userExists = await _context.Users.AnyAsync(u => u.Username == userDto.Username);
            if (userExists)
            {
                return BadRequest(new { Message = "Username already exists!" });
            }

            var user = new User
            {
                Username = userDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                ProfileImage = userDto.ProfileImage,
                CreatedOn=DateTime.UtcNow,
                CreatedBy=userDto.Username,
                IsActive=true,
               ModifiedBy=null,
                ModifiedOn=null
                
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userloginDto)
        {
            if (userloginDto == null || string.IsNullOrWhiteSpace(userloginDto.Username) || string.IsNullOrWhiteSpace(userloginDto.Password))
            {
                return BadRequest(new { Message = "Invalid user data!" });
            }

            var user = _context.Users.SingleOrDefault(u => u.Username == userloginDto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userloginDto.Password, user.Password))
            {
                return Unauthorized(new { Message = "Invalid username or password!" });
            }
            var token = GenerateJwtToken(user);
            return Ok(new {Token=token });
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.Select(user => new
            {
                user.Id,
                user.Username,
                user.ProfileImage

            }).ToListAsync();
            return Ok(users);
        }

    }
}
   
