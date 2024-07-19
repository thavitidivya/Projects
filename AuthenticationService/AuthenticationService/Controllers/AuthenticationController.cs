using AuthenticationService.Context;
using AuthenticationService.Models;
using AuthenticationService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IJwtServicecs jwtServicecs;

        public
            AuthenticationController(UserContext context, IJwtServicecs jwtService)
        {
            _context = context;
            jwtServicecs = jwtService;
        }
        private string GenerateJwtToken(User user)
        {
            return jwtServicecs.GenerateToken(user.Username);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
        {
            if (userDto == null || string.IsNullOrWhiteSpace(userDto.Username) || string.IsNullOrWhiteSpace(userDto.Password))
            {
                return BadRequest(new { Message = "Invalid user data!" });
            }

            var userExists = await _context.Authuserss.AnyAsync(u => u.Username == userDto.Username);
            if (userExists)
            {
                return BadRequest(new { Message = "Username already exists!" });
            }

            var user = new User
            {
                Username = userDto.Username,
                Email=userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                ConfirmPassword=BCrypt.Net.BCrypt.HashPassword(userDto.ConfirmPassword),
                ProfileImage = userDto.ProfileImage,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userDto.Username,
                IsActive = true,
                ModifiedBy = null,
                ModifiedOn = null

            };

            _context.Authuserss.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userloginDto)
        {
            if (userloginDto == null || string.IsNullOrWhiteSpace(userloginDto.Email) || string.IsNullOrWhiteSpace(userloginDto.Password))
            {
                return BadRequest(new { Message = "Invalid user data!" });
            }

            var user = _context.Authuserss.SingleOrDefault(u => u.Email == userloginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userloginDto.Password, user.Password))
            {
                return Unauthorized(new { Message = "Invalid Email or password!" });
            }
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
    }
}
