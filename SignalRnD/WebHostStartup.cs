using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SignalRnD;

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
