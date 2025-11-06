using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace ApiDemo.Tests.Infrastructure;

public class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    protected readonly HttpClient Client;
    protected readonly CustomWebApplicationFactory<Program> Factory;
    protected readonly JsonSerializerOptions JsonOptions;

    public IntegrationTestBase(CustomWebApplicationFactory<Program> factory)
    {
     Factory = factory;
        Client = factory.CreateClient();
   
  JsonOptions = new JsonSerializerOptions
        {
         PropertyNameCaseInsensitive = true
        };
    }

    protected async Task<T?> DeserializeResponse<T>(HttpResponseMessage response)
    {
   var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, JsonOptions);
    }

    protected StringContent CreateJsonContent(object obj)
    {
   var json = JsonSerializer.Serialize(obj);
        return new StringContent(json, System.Text.Encoding.UTF8, "application/json");
    }

    public void Dispose()
    {
        Client?.Dispose();
    }
}
