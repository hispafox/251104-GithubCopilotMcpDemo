using Microsoft.AspNetCore.Mvc;
using ApiDemo.Application.DTOs;
using ApiDemo.Application.Services;
using FluentValidation;

namespace ApiDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IValidator<CreateTaskDto> _createValidator;
    private readonly IValidator<UpdateTaskDto> _updateValidator;

    public TasksController(
        ITaskService taskService,
        IValidator<CreateTaskDto> createValidator,
        IValidator<UpdateTaskDto> updateValidator)
    {
  _taskService = taskService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    /// <summary>
    /// Get all tasks
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
    {
        var tasks = await _taskService.GetAllTasksAsync();
  return Ok(tasks);
    }

    /// <summary>
  /// Get task by ID
/// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> GetTaskById(Guid id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
      return NotFound(new { message = "Task not found" });

     return Ok(task);
    }

    /// <summary>
    /// Create a new task
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaskDto>> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
    var validationResult = await _createValidator.ValidateAsync(createTaskDto);
        if (!validationResult.IsValid)
        {
    return BadRequest(new
         {
    statusCode = 400,
         message = "Validation failed",
         errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            });
}

var task = await _taskService.CreateTaskAsync(createTaskDto);
  return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    /// <summary>
    /// Update an existing task
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> UpdateTask(Guid id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        var validationResult = await _updateValidator.ValidateAsync(updateTaskDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(new
        {
     statusCode = 400,
          message = "Validation failed",
   errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
 });
        }

        var task = await _taskService.UpdateTaskAsync(id, updateTaskDto);
        if (task == null)
          return NotFound(new { message = "Task not found" });

        return Ok(task);
    }

    /// <summary>
    /// Delete a task
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var deleted = await _taskService.DeleteTaskAsync(id);
        if (!deleted)
   return NotFound(new { message = "Task not found" });

      return NoContent();
    }

 /// <summary>
    /// Get tasks by status
    /// </summary>
    [HttpGet("status/{status}")]
    [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByStatus(string status)
    {
        if (!Enum.TryParse<ApiDemo.Core.Enums.TaskStatus>(status, true, out var taskStatus))
        {
return BadRequest(new
 {
     statusCode = 400,
  message = "Invalid status",
       errors = new[] { "Status must be Pending, InProgress, or Completed" }
     });
        }

        var tasks = await _taskService.GetTasksByStatusAsync(taskStatus);
  return Ok(tasks);
    }
}
