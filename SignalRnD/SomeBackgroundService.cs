using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace SignalRnD;

/// <summary>
/// Noddy background service.
/// - Listens for a 'msg' message and echos it.
/// - Sends a 'Heartbeat' message every 10 seconds.
/// </summary>
public class SomeBackgroundService : BackgroundService
{
    private readonly HubConnection _connection;
    private readonly IConfiguration _cfg;

    public SomeBackgroundService(IConfiguration cfg)
    {
        _cfg = cfg;
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/testHub")
            .WithAutomaticReconnect()
            .Build();

        _connection.On("msg", (string message) =>
        {
            Console.WriteLine("msg: " + message);
        });
    }


    protected override async Task ExecuteAsync(CancellationToken cnx)
    {
        Console.WriteLine("SomeBackgroundService Execute");

        // Local SignalR 
        await _connection.StartAsync(cnx);

        while (!cnx.IsCancellationRequested)
        {
            Console.WriteLine("tick");

            // Invoke the 'Heartbeat' method on the hub, passing in a message.
            _connection?.InvokeAsync("Heartbeat", $"Heartbeat: {DateTime.Now:HH:mm:ss}", cnx);

            await Task.Delay(10000, cnx);
        }
    }
}
