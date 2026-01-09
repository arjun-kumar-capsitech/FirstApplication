using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Project_manager.Models;
using Project_manager.Services;

namespace Project_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        private static readonly Regex SpaceRegex = new Regex(@"\s+", RegexOptions.Compiled);
        private string Clean(string input)
        {
            return SpaceRegex.Replace(input ?? "", " ").Trim();
        }

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectDto dto)
        {
            dto.ProjectName = Clean(dto.ProjectName);
            dto.Description = Clean(dto.Description);
            dto.AssignUser = Clean(dto.AssignUser);

            var newProject = new Project
            {
                ProjectName = dto.ProjectName,
                Description = dto.Description,
                AssignUser = dto.AssignUser
            };

            await _projectService.AddProjectAsync(newProject);

            return CreatedAtAction(nameof(GetById), new { id = newProject.Id }, newProject);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] ProjectDto dto)
        {
            var existing = await _projectService.GetProjectByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.ProjectName = Clean(dto.ProjectName);
            existing.Description = Clean(dto.Description);
            existing.AssignUser = Clean(dto.AssignUser);

            await _projectService.UpdateProjectAsync(id, existing);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _projectService.GetProjectByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
