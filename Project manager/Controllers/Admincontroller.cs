using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project_manager.Services;
using Project_manager.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly RegService _regService;
        private readonly IConfiguration _config;

        public LoginController(RegService regService, IConfiguration config)
        {
            _regService = regService;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AdminLoginDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid request");

            var users = await _regService.GetRegsAsync();
            var user = users.FirstOrDefault(u =>
                u.Username == dto.Username && u.Password == dto.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            var token = GenerateToken(user.Username);

            return Ok(new
            {
                username = user.Username,
                token = token
            });
        }

        private string GenerateToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
