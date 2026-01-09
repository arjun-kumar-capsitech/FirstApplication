using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Project_manager.Models;
using Project_manager.Services;

namespace Project_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        private static readonly Regex SpaceRegex = new Regex(@"\s+", RegexOptions.Compiled);

        private string Clean(string input)
        {
            return SpaceRegex.Replace(input ?? "", " ").Trim();
        }

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Userdto dto)
        {
           
            dto.Name = Clean(dto.Name);
            dto.Email = Clean(dto.Email);

            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Role = dto.Role
            };

            await _userService.AddUserAsync(newUser);

            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] Userdto dto)
        {
            var existing = await _userService.GetUserByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.Name = Clean(dto.Name);
            existing.Email = Clean(dto.Email);
            existing.Role = dto.Role;

            await _userService.UpdateUserAsync(id, existing);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _userService.GetUserByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _userService.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
