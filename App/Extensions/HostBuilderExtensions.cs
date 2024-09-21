using System;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Sinks.Udp.TextFormatters;

namespace App.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseRandomConfigSerilog(this IHostBuilder builder)
    {
        var randomValue = Random.Shared.Next();
        return randomValue % 2 == 0
            ? builder.UseJsonConfigSerilog() 
            : builder.UseFluentConfigSerilog();
    }

    private static IHostBuilder UseJsonConfigSerilog(this IHostBuilder builder)
    {
        Console.WriteLine("Using json configuration");
        
        return builder.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            SelfLog.Enable(Console.Error);

            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext();
        });
    }

    private static IHostBuilder UseFluentConfigSerilog(this IHostBuilder builder)
    {
        Console.WriteLine("Using fluent configuration");
        
        return builder.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            SelfLog.Enable(Console.Error);

            var filePath = hostingContext.Configuration.GetFilePath();
            var remotePort = hostingContext.Configuration.GetRemotePort();
            var addressFamily = hostingContext.Configuration.GetAddressFamily();
            var remoteAddress = hostingContext.Configuration.GetRemoteAddress();
            var outputTemplate = hostingContext.Configuration.GetOutputTemplate();
            var serverUrl = hostingContext.Configuration.GetServerUrl();
            var connectionString = hostingContext.Configuration.GetConnectionString();

            loggerConfiguration
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: outputTemplate)
                .WriteTo.File(filePath, rollingInterval: RollingInterval.Day)
                .WriteTo.Udp(remoteAddress, remotePort, addressFamily, new Log4jTextFormatter())
                .WriteTo.Seq(serverUrl)
                .WriteTo.ApplicationInsights(connectionString, TelemetryConverter.Traces);
        });
    }
}