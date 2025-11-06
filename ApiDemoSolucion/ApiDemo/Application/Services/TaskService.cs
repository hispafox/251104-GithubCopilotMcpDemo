using AutoMapper;
using ApiDemo.Application.DTOs;
using ApiDemo.Core.Entities;
using ApiDemo.Core.Interfaces;
using TaskStatus = ApiDemo.Core.Enums.TaskStatus;

namespace ApiDemo.Application.Services;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
    {
        var tasks = await _unitOfWork.Tasks.GetAllAsync();
        return _mapper.Map<IEnumerable<TaskDto>>(tasks);
    }

    public async Task<TaskDto?> GetTaskByIdAsync(Guid id)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id);
        return task == null ? null : _mapper.Map<TaskDto>(task);
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
    {
    var taskEntity = _mapper.Map<TaskEntity>(createTaskDto);
        taskEntity.Id = Guid.NewGuid();
        
  await _unitOfWork.Tasks.AddAsync(taskEntity);
        await _unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<TaskDto>(taskEntity);
    }

    public async Task<TaskDto?> UpdateTaskAsync(Guid id, UpdateTaskDto updateTaskDto)
    {
        var existingTask = await _unitOfWork.Tasks.GetByIdAsync(id);
        if (existingTask == null)
          return null;

     _mapper.Map(updateTaskDto, existingTask);
  existingTask.UpdatedAt = DateTime.UtcNow;
        
        _unitOfWork.Tasks.Update(existingTask);
 await _unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<TaskDto>(existingTask);
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id);
        if (task == null)
            return false;

        _unitOfWork.Tasks.Delete(task);
        await _unitOfWork.SaveChangesAsync();
     
        return true;
    }

    public async Task<IEnumerable<TaskDto>> GetTasksByStatusAsync(ApiDemo.Core.Enums.TaskStatus status)
  {
        var tasks = await _unitOfWork.Tasks.FindAsync(t => t.Status == status);
return _mapper.Map<IEnumerable<TaskDto>>(tasks);
    }
}
