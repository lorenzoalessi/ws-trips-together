using Microsoft.EntityFrameworkCore;
using WsTripsTogether.Model;

namespace WsTripsTogether;

public class Startup(WebApplicationBuilder builder)
{
    public async Task ConfigureApplication()
    {
        _ = builder.Services.AddControllers();

        // Add services and repositories to container of dependency injection 
        ConfigureServices(builder.Services);
        ConfigureRepositories(builder.Services);

        // DB setup
        await ConfigureDb();
    }

    public void ApplyConfiguration(IApplicationBuilder app)
    {
        _ = app.UseRouting();
        _ = app.UseAuthorization();
        _ = app.UseEndpoints(x => x.MapControllers());
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // _ = services.AddScoped<>()
    }

    private void ConfigureRepositories(IServiceCollection serices)
    {
        // _ = serices.AddScoped<>()
    }

    private async Task ConfigureDb()
    {
        _ = builder.Services.AddDbContext<Context>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")));

        await using var serviceProvider = builder.Services.BuildServiceProvider();
        // Migrations
        await serviceProvider.GetRequiredService<Context>().Database.MigrateAsync();
    }
}