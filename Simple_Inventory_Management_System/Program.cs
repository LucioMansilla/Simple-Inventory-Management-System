using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Simple_Inventory_Management_System;

public static class Program
{
    private static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var serviceCollection = new ServiceCollection();
        var startup = new Startup(configuration);
        startup.ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var app = serviceProvider.GetRequiredService<Application>();
        app.Run();
    }
}