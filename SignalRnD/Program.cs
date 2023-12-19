using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace SignalRnD;

/// <summary>
/// Server
/// </summary>
internal static class Program
{
    static void Main(string[] args)
    {
        Host.CreateDefaultBuilder(args)
            .UseWindowsService(cfg => { 
                cfg.ServiceName = "SignalRnD";
            })
            .ConfigureWebHostDefaults(cfg =>
            {
                cfg.UseStartup<WebHostStartup>();
            })
            .Build()
            .Run();
    }
}
