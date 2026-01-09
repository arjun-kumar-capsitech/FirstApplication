using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Project_manager.Models;
using Project_manager.Services;

namespace Project_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskasController : ControllerBase
    {
        private readonly TaskasService _taskasService;

        private static readonly Regex SpaceRegex = new Regex(@"\s+", RegexOptions.Compiled);

        private string Clean(string input)
        {
            return SpaceRegex.Replace(input ?? "", " ").Trim();
        }

        public TaskasController(TaskasService taskasService)
        {
            _taskasService = taskasService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskasService.GetAllTaskasAsync();
            return Ok(tasks);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var task = await _taskasService.GetTaskasByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskasDto dto)
        {
            
            dto.Title = Clean(dto.Title);
            dto.Description = Clean(dto.Description);
            dto.Project = Clean(dto.Project);

            var newTask = new Taskas
            {
                Title = dto.Title,
                Description = dto.Description,
                Project = dto.Project,
                Status = dto.Status
            };

            await _taskasService.AddTaskasAsync(newTask);

            return CreatedAtAction(nameof(GetById), new { id = newTask.Id }, newTask);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] TaskasDto dto)
        {
            var existing = await _taskasService.GetTaskasByIdAsync(id);
            if (existing == null)
                return NotFound();

            
            existing.Title = Clean(dto.Title);
            existing.Description = Clean(dto.Description);
            existing.Project = Clean(dto.Project);
            existing.Status = dto.Status;

            await _taskasService.UpdateTaskasAsync(id, existing);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _taskasService.GetTaskasByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _taskasService.DeleteTaskasAsync(id);

            return NoContent();
        }
    }
}
