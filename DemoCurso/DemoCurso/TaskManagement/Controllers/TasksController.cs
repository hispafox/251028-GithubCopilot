using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private static readonly List<TaskItem> Tasks = new();

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(Tasks);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskItem task)
        {
            if (task == null || string.IsNullOrWhiteSpace(task.Description) || task.Deadline == default)
            {
                return BadRequest("Invalid task data.");
            }

            Tasks.Add(task);
            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
        }
    }

    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
    }
}