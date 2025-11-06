using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApiDemo.Infrastructure.Data;

namespace ApiDemo.Tests.Infrastructure;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> 
    where TStartup : class
{
    private static int _databaseCounter = 0;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
     builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext registration
    var descriptor = services.SingleOrDefault(
     d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
     {
     services.Remove(descriptor);
     }

   // Add DbContext using InMemory database for testing with unique name per instance
    var databaseName = $"InMemoryTestDb_{Interlocked.Increment(ref _databaseCounter)}";
       services.AddDbContext<ApplicationDbContext>(options =>
            {
      options.UseInMemoryDatabase(databaseName);
    });

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

   // Create a scope to get the database context
     using (var scope = serviceProvider.CreateScope())
     {
      var scopedServices = scope.ServiceProvider;
          var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                // Ensure the database is created
    db.Database.EnsureCreated();
            }
        });

        builder.UseEnvironment("Testing");
    }
}
