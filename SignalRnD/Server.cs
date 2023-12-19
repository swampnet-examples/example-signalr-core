using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

namespace SignalRnD;

internal static class Server
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        host.Run();
    }


    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<WebHostStartup>();
            });
    }         
}


/// <summary>
/// AspNetCore Startup class
/// </summary>
public class WebHostStartup
{
    public IConfiguration Configuration { get; }

    public WebHostStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR();
        services.AddHostedService<SomeBackgroundService>();
    }


    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<MessageHub>("/testHub");
        });
    }
}
