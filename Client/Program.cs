using Microsoft.AspNetCore.SignalR.Client;

namespace Client;

internal static class Program
{
    private static HubConnection? _connection;
    private static HubConnectionBuilder? _connectionBuilder;

    static async Task Main(string[] args)
    {
        _connectionBuilder = new HubConnectionBuilder();

        _connection = _connectionBuilder
            .WithUrl("http://localhost:5000/testHub")
            .WithAutomaticReconnect()
            .Build();


        #region Just subscribe and log various events

        _connection.Closed += (error) =>
        {
            Console.WriteLine($"Connection closed: {error?.Message}");
            return Task.CompletedTask;
        };

        _connection.Reconnecting += (error) =>
        {
            Console.WriteLine($"Connection reconnecting: {error?.Message}");
            return Task.CompletedTask;
        };

        _connection.Reconnected += (connectionId) =>
        {
            Console.WriteLine($"Connection reconnected: id: {connectionId}");
            return Task.CompletedTask;
        };

        #endregion


        _connection.On("msg", (string message) =>
        {
            Console.WriteLine("msg: " + message);
        });

        _connection.On("hb", (string message) =>
        {
            Console.WriteLine("hb: " + message);
        });

        await _connection.StartAsync();

        Console.WriteLine("Type something to broadcast a message to all connected clients.");

        while (true)
        {
            var message = Console.ReadLine();
            if(message != null)
            {
                // Invoke SendMessage on the hub, passing in a message.
                _connection?.InvokeAsync("SendMessage", message);
            }
        }
    }
}
