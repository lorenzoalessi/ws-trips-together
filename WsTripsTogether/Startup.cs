using Microsoft.EntityFrameworkCore;
using WsTripsTogether.Model;
using WsTripsTogether.Repository.User;
using WsTripsTogether.Seeder;
using WsTripsTogether.Services.Login;
using WsTripsTogether.Services.User;

namespace WsTripsTogether;

public class Startup(WebApplicationBuilder builder)
{
    public async Task ConfigureApplication()
    {
        _ = builder.Services.AddControllers();

        builder.Services.AddAutoMapper(typeof(Startup));
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

    private static void ConfigureServices(IServiceCollection services)
    {
        // Add repository to container dependency injection
        _ = services.AddScoped<DbInitializer>()
            .AddScoped<IUserService, UserService>();
        
        // Singleton
        _ = services.AddSingleton<ILoginHandler, LoginHandler>();
    }

    private static void ConfigureRepositories(IServiceCollection services)
    {
        // Add repository to container dependency injection
        _ = services.AddScoped<IUserRepository, UserRepository>();
    }

    private async Task ConfigureDb()
    {
        _ = builder.Services.AddDbContext<Context>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")));

        await using var serviceProvider = builder.Services.BuildServiceProvider();
        // Migrations
        await serviceProvider.GetRequiredService<Context>().Database.MigrateAsync();
        // Initialize DB with first data
        await serviceProvider.GetRequiredService<DbInitializer>().InitDb();
    }
}