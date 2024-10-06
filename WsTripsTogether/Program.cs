namespace WsTripsTogether;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var startup = new Startup(builder);
        await startup.ConfigureApplication();

        var app = builder.Build();
        startup.ApplyConfiguration(app);
        await app.RunAsync();
    }
}