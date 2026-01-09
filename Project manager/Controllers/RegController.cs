using Microsoft.AspNetCore.Mvc;
using Project_manager.Models;
using Project_manager.Services;

namespace Project_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegController : ControllerBase
    {
        private readonly RegService _regService;

        public RegController(RegService regService)
        {
            _regService = regService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Reg reg)
        {
            // ✅ ConfirmPassword check
            if (reg.Password != reg.ConfirmPassword)
            {
                return BadRequest("Password and Confirm Password do not match!");
            }

            await _regService.AddRegAsync(reg);
            return CreatedAtAction(nameof(Create), new { id = reg.Id }, reg);
        }

        [HttpGet]
        public async Task<ActionResult<List<Reg>>> GetAll()
        {
            var regs = await _regService.GetRegsAsync();
            return Ok(regs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reg>> GetById(string id)
        {
            var reg = await _regService.GetRegByIdAsync(id);
            if (reg == null) return NotFound();
            return Ok(reg);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Reg updatedReg)
        {
            var existing = await _regService.GetRegByIdAsync(id);
            if (existing == null) return NotFound();

            if (updatedReg.Password != updatedReg.ConfirmPassword)
            {
                return BadRequest("Password and Confirm Password do not match!");
            }

            updatedReg.Id = id;
            await _regService.UpdateRegAsync(id, updatedReg);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existing = await _regService.GetRegByIdAsync(id);
            if (existing == null) return NotFound();

            await _regService.DeleteRegAsync(id);
            return NoContent();
        }
    }
}
