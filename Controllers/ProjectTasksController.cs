using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.API.Models;

namespace TaskTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksController : ControllerBase
    {
        private readonly ProjectContext _context;

        public ProjectTasksController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/ProjectTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreateTaskDTO>>> GetProjectTasks()
        {
            return await _context.ProjectTasks
                .Select(x => ProjectToDTO(x))
                .ToListAsync();
        }

        // GET: api/ProjectTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreateTaskDTO>> GetProjectTask(long id)
        {
            var projectTask = await _context.ProjectTasks.FindAsync(id);

            if (projectTask == null)
            {
                return NotFound();
            }

            return ProjectToDTO(projectTask);
        }

        // PUT: api/ProjectTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTask(long id, CreateTaskDTO projectDTO)
        {
            if (id != projectDTO.Id)
            {
                return BadRequest();
            }

            var projectTask = await _context.ProjectTasks.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }

            projectTask.Name = projectDTO.Name;
            projectTask.IsComplete = projectDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProjectTaskExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/ProjectTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateTaskDTO>> PostProjectTask(CreateTaskDTO projectDTO)
        {
            var projectTask = new ProjectTask
            {
                IsComplete = projectDTO.IsComplete,
                Name = projectDTO.Name
            };

            _context.ProjectTasks.Add(projectTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProjectTask),
                new { id = projectTask.ID},
                ProjectToDTO(projectTask));
        }

        // DELETE: api/ProjectTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTask(long id)
        {
            var projectTask = await _context.ProjectTasks.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }

            _context.ProjectTasks.Remove(projectTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectTaskExists(long id)
        {
            return _context.ProjectTasks.Any(e => e.ID == id);
        }

        private static CreateTaskDTO ProjectToDTO(ProjectTask projectTask) =>
            new CreateTaskDTO
            {
                Id = projectTask.ID,
                Name = projectTask.Name,
                IsComplete = projectTask.IsComplete
            };
    }
}