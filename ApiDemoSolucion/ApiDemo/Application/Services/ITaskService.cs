using ApiDemo.Application.DTOs;

namespace ApiDemo.Application.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllTasksAsync();
    Task<TaskDto?> GetTaskByIdAsync(Guid id);
    Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto);
    Task<TaskDto?> UpdateTaskAsync(Guid id, UpdateTaskDto updateTaskDto);
    Task<bool> DeleteTaskAsync(Guid id);
    Task<IEnumerable<TaskDto>> GetTasksByStatusAsync(ApiDemo.Core.Enums.TaskStatus status);
}
