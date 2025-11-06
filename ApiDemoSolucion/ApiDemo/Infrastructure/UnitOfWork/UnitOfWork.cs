using ApiDemo.Core.Entities;
using ApiDemo.Core.Interfaces;
using ApiDemo.Infrastructure.Data;
using ApiDemo.Infrastructure.Repositories;

namespace ApiDemo.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
  private readonly ApplicationDbContext _context;
  private IRepository<TaskEntity>? _tasks;

    public UnitOfWork(ApplicationDbContext context)
{
        _context = context;
    }

    public IRepository<TaskEntity> Tasks
    {
      get
        {
 return _tasks ??= new Repository<TaskEntity>(_context);
     }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
    _context.Dispose();
    }
}
