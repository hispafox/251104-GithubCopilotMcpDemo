using TaskStatusEnum = ApiDemo.Core.Enums.TaskStatus;
using TaskPriorityEnum = ApiDemo.Core.Enums.TaskPriority;

namespace ApiDemo.Core.Entities;

public class TaskEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskStatusEnum Status { get; set; }
    public TaskPriorityEnum Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
