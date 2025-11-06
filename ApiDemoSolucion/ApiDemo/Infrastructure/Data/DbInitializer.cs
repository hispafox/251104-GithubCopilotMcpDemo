using ApiDemo.Core.Entities;
using ApiDemo.Infrastructure.Data;
using TaskStatusEnum = ApiDemo.Core.Enums.TaskStatus;
using TaskPriorityEnum = ApiDemo.Core.Enums.TaskPriority;

namespace ApiDemo.Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
{
        context.Database.EnsureCreated();

        // Check if database has been seeded
        if (context.Tasks.Any())
        {
            return;   // DB has been seeded
        }

    var tasks = new TaskEntity[]
        {
      new TaskEntity
            {
     Id = Guid.NewGuid(),
          Title = "Completar documentación del proyecto",
          Description = "Escribir README y documentación técnica completa",
                Status = TaskStatusEnum.InProgress,
  Priority = TaskPriorityEnum.High,
 DueDate = DateTime.UtcNow.AddDays(7),
    CreatedAt = DateTime.UtcNow.AddDays(-2),
UpdatedAt = DateTime.UtcNow.AddDays(-1)
},
       new TaskEntity
   {
         Id = Guid.NewGuid(),
       Title = "Revisar código y optimizar queries",
   Description = "Realizar code review y optimizar consultas a base de datos",
       Status = TaskStatusEnum.Pending,
        Priority = TaskPriorityEnum.Medium,
            DueDate = DateTime.UtcNow.AddDays(14),
        CreatedAt = DateTime.UtcNow.AddDays(-1),
     UpdatedAt = DateTime.UtcNow.AddDays(-1)
            },
          new TaskEntity
            {
  Id = Guid.NewGuid(),
  Title = "Implementar autenticación JWT",
     Description = "Agregar seguridad con tokens JWT para la API",
     Status = TaskStatusEnum.Pending,
                Priority = TaskPriorityEnum.High,
   DueDate = DateTime.UtcNow.AddDays(10),
     CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
     },
            new TaskEntity
         {
      Id = Guid.NewGuid(),
          Title = "Configurar CI/CD",
         Description = "Configurar pipeline de integración y despliegue continuo",
    Status = TaskStatusEnum.Pending,
    Priority = TaskPriorityEnum.Low,
              DueDate = DateTime.UtcNow.AddDays(30),
    CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
 },
            new TaskEntity
   {
     Id = Guid.NewGuid(),
                Title = "Implementar tests unitarios",
     Description = "Crear suite de tests unitarios con xUnit",
                Status = TaskStatusEnum.InProgress,
     Priority = TaskPriorityEnum.High,
                DueDate = DateTime.UtcNow.AddDays(5),
             CreatedAt = DateTime.UtcNow.AddDays(-3),
       UpdatedAt = DateTime.UtcNow
            },
 new TaskEntity
  {
                Id = Guid.NewGuid(),
                Title = "Actualizar dependencias NuGet",
       Description = "Revisar y actualizar paquetes NuGet a las últimas versiones",
       Status = TaskStatusEnum.Completed,
  Priority = TaskPriorityEnum.Low,
           DueDate = DateTime.UtcNow.AddDays(-2),
         CreatedAt = DateTime.UtcNow.AddDays(-5),
          UpdatedAt = DateTime.UtcNow.AddDays(-2)
            }
        };

        context.Tasks.AddRange(tasks);
        context.SaveChanges();
    }
}
