using ApiDemo.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiDemo.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
  : base(options)
    {
    }

    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

     modelBuilder.Entity<TaskEntity>(entity =>
        {
    entity.ToTable("Tasks");
  
            entity.HasKey(e => e.Id);
            
        entity.Property(e => e.Title)
                .IsRequired()
   .HasMaxLength(200);
            
        entity.Property(e => e.Description)
         .HasMaxLength(1000);
 
            entity.Property(e => e.Status)
    .IsRequired()
       .HasConversion<string>();
            
            entity.Property(e => e.Priority)
    .IsRequired()
        .HasConversion<string>();
       
            entity.Property(e => e.CreatedAt)
           .IsRequired();
         
       entity.Property(e => e.UpdatedAt)
       .IsRequired();
        });
    }
}
