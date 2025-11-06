using AutoMapper;
using ApiDemo.Core.Entities;
using ApiDemo.Application.DTOs;
using TaskStatusEnum = ApiDemo.Core.Enums.TaskStatus;
using TaskPriorityEnum = ApiDemo.Core.Enums.TaskPriority;

namespace ApiDemo.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
 // Entity to DTO
        CreateMap<TaskEntity, TaskDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()));

 // CreateDTO to Entity
        CreateMap<CreateTaskDto, TaskEntity>()
      .ForMember(dest => dest.Id, opt => opt.Ignore())
       .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<TaskStatusEnum>(src.Status)))
.ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<TaskPriorityEnum>(src.Priority)))
    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
      .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

  // UpdateDTO to Entity
      CreateMap<UpdateTaskDto, TaskEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<TaskStatusEnum>(src.Status)))
     .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<TaskPriorityEnum>(src.Priority)))
    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
       .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}
