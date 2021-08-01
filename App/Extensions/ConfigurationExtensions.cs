using System.Net.Sockets;
using Microsoft.Extensions.Configuration;

namespace App.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetOutputTemplate(this IConfiguration configuration)
        {
            return configuration["Serilog:WriteTo:0:Args:outputTemplate"];
        }

        public static string GetFilePath(this IConfiguration configuration)
        {
            return configuration["Serilog:WriteTo:1:Args:path"];
        }

        public static int GetRemotePort(this IConfiguration configuration)
        {
            return configuration.GetValue<int>("Serilog:WriteTo:2:Args:remotePort");
        }

        public static string GetRemoteAddress(this IConfiguration configuration)
        {
            return configuration["Serilog:WriteTo:2:Args:remoteAddress"];
        }

        public static AddressFamily GetAddressFamily(this IConfiguration configuration)
        {
            return configuration.GetValue<AddressFamily>("Serilog:WriteTo:2:Args:family");
        }
    }
}