using Microsoft.AspNetCore.SignalR.Client;

namespace Client;

/// <summary>
/// Client
/// </summary>
internal static class Program
{
    static async Task Main(string[] args)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/testHub")
            .WithAutomaticReconnect()
            .Build();

        // Do something when we receive a 'msg' message.
        connection.On("msg", (string message) =>
        {
            Console.WriteLine("msg: " + message);
        });


        // Do something when we receive a 'hb' message.
        connection.On("hb", (string message) =>
        {
            Console.WriteLine("hb: " + message);
        });

        await connection.StartAsync();


        Console.WriteLine("Type something to broadcast a message to all connected clients.");
        while (true)
        {
            var message = Console.ReadLine();
            if(message != null)
            {
                // Invoke SendMessage method on the hub, passing in a message.
                connection?.InvokeAsync("SendMessage", message);
            }
        }
    }
}
