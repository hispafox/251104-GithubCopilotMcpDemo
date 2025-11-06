using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ApiDemo.Infrastructure.Data;
using ApiDemo.Core.Interfaces;
using ApiDemo.Infrastructure.UnitOfWork;
using ApiDemo.Application.Services;
using ApiDemo.Application.Mappings;
using ApiDemo.Application.Validators;
using ApiDemo.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Database Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository and Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Application Services
builder.Services.AddScoped<ITaskService, TaskService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskDtoValidator>();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
    Title = "Task Management API",
        Version = "v1",
        Description = "API RESTful para gestión de tareas con .NET 8",
        Contact = new() { Name = "API Demo", Email = "contact@taskmanagement.com" }
    });
});

var app = builder.Build();

// Seed Database (Uncomment to initialize with sample data)
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     var context = services.GetRequiredService<ApplicationDbContext>();
//     DbInitializer.Initialize(context);
// }

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API v1");
        c.RoutePrefix = string.Empty; // Swagger UI at root
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Make Program class accessible for testing
public partial class Program { }
