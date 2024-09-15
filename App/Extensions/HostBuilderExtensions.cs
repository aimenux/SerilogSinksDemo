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
        return builder.UseJsonConfigSerilog();
        var randomValue = Random.Shared.Next();
        return randomValue % 2 == 0
            ? builder.UseJsonConfigSerilog() 
            : builder.UseFluentConfigSerilog();
    }

    private static IHostBuilder UseJsonConfigSerilog(this IHostBuilder builder)
    {
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
        return builder.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            SelfLog.Enable(Console.Error);

            var filePath = hostingContext.Configuration.GetFilePath();
            var remotePort = hostingContext.Configuration.GetRemotePort();
            var addressFamily = hostingContext.Configuration.GetAddressFamily();
            var remoteAddress = hostingContext.Configuration.GetRemoteAddress();
            var outputTemplate = hostingContext.Configuration.GetOutputTemplate();

            loggerConfiguration
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: outputTemplate)
                .WriteTo.File(filePath, rollingInterval: RollingInterval.Day)
                .WriteTo.Udp(remoteAddress, remotePort, addressFamily, new Log4jTextFormatter());
        });
    }
}