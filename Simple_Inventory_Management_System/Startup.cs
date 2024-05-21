using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Simple_Inventory_Management_System.DataBase;

namespace Simple_Inventory_Management_System;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<DatabaseSettings>(Configuration.GetSection("ConnectionStrings"));

        services.AddSingleton<SqlServerStrategy>(provider =>
        {
            var settings = provider.GetService<IOptions<DatabaseSettings>>().Value;
            Console.WriteLine($"SQL Server Connection String: {settings.SqlServer}");
            return new SqlServerStrategy(settings.SqlServer);
        });

        services.AddSingleton<MongoDbStrategy>(provider =>
        {
            var settings = provider.GetService<IOptions<DatabaseSettings>>().Value;
            Console.WriteLine($"MongoDB Connection String: {settings.MongoDb}");
            return new MongoDbStrategy(settings.MongoDb);
        });

        services.AddSingleton<DatabaseContext>(provider =>
        {
            var sqlServerStrategy = provider.GetService<SqlServerStrategy>();
            var context = new DatabaseContext(sqlServerStrategy);
            return context;
        });

        services.AddSingleton<Inventory>();
        services.AddSingleton<Application>();
    }
}