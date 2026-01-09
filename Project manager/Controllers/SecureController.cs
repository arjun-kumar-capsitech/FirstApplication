using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecureController : ControllerBase
    {
        [Authorize]
        [HttpGet("data")]
        public IActionResult GetSecureData()
        {
            return Ok("This is protected data, only accessible with JWT token!");
        }
    }
}
