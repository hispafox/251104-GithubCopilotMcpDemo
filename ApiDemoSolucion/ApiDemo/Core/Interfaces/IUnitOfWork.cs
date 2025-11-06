using ApiDemo.Core.Entities;

namespace ApiDemo.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<TaskEntity> Tasks { get; }
    Task<int> SaveChangesAsync();
}
