using Microsoft.AspNetCore;

namespace KaseyWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureAppConfiguration(config =>
                config.AddEnvironmentVariables()
            )
            .Build();
    }
}