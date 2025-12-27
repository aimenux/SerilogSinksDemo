using System.Net.Sockets;
using Microsoft.Extensions.Configuration;

namespace App.Extensions;

public static class ConfigurationExtensions
{
    extension(IConfiguration configuration)
    {
        public string GetOutputTemplate()
        {
            return configuration["Serilog:WriteTo:0:Args:outputTemplate"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GetFilePath()
        {
            return configuration.GetValue<string>("Serilog:WriteTo:1:Args:path") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public int GetRemotePort()
        {
            return configuration.GetValue<int?>("Serilog:WriteTo:2:Args:remotePort") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GetRemoteAddress()
        {
            return configuration.GetValue<string>("Serilog:WriteTo:2:Args:remoteAddress") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public AddressFamily GetAddressFamily()
        {
            return configuration.GetValue<AddressFamily?>("Serilog:WriteTo:2:Args:family") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GetServerUrl()
        {
            return configuration.GetValue<string>("Serilog:WriteTo:3:Args:serverUrl") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GetConnectionString()
        {
            return configuration.GetValue<string>("Serilog:WriteTo:4:Args:connectionString") ?? throw new ArgumentNullException(nameof(configuration));
        }
    }
}