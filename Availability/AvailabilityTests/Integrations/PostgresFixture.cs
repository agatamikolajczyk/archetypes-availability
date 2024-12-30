using System.Reflection;
using Testcontainers.PostgreSql;

namespace AvailabilityTests.Integrations;


public class PostgresFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .Build();

    public string ConnectionString
    {
        get { return _postgres.GetConnectionString(); }
    }

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
        await ExecuteResourceScripts();
    }

    public async Task DisposeAsync()
    {
        await _postgres.DisposeAsync();
    }

    private async Task ExecuteResourceScripts()
    {
        var dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var resourcesPath = Path.Combine(dirPath, "Resources");
        
        var files = Directory.GetFiles(resourcesPath, "*.sql");
            
        foreach (var file in files)
        {
            var content = await File.ReadAllTextAsync(file);
            await _postgres.ExecScriptAsync(content);
        }
    }
}