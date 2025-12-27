using App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .AddServices(args)
            .Build();
        
        var service = host.Services.GetRequiredService<IDummyService>();
        await service.DoNothingAsync();
        
        Console.WriteLine("Press any key to exit !");
        Console.ReadKey();
    }
}