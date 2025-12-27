using System.Reflection;
using App.Extensions;
using App.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App;

public static class DependencyInjection
{
    public static IHostBuilder AddServices(this IHostBuilder builder, string[] args)
    {
        return builder
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(GetDirectoryPath());
                config.AddJsonFile("appsettings.json");
                config.AddUserSecrets(context);
                config.AddEnvironmentVariables();
                config.AddCommandLine(args);
            })
            .ConfigureServices((_, services) =>
            {
                services.AddTransient<IDummyService, DummyService>();
            })
            .ConfigureSerilog();
    }
    
    private static void AddUserSecrets(this IConfigurationBuilder configurationBuilder, HostBuilderContext context)
    {
        if (context.HostingEnvironment.IsDevelopment())
        {
            configurationBuilder.AddUserSecrets(typeof(Program).Assembly);
        }
    }
    
    private static string GetDirectoryPath()
    {
        try
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        }
        catch
        {
            return Directory.GetCurrentDirectory();
        }
    }
}