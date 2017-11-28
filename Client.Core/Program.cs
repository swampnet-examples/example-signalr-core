using Microsoft.AspNetCore.SignalR.Client;
using Shared;
using System;
using System.Threading.Tasks;

namespace Client.Core
{
    static class Program
    {
        private static HubConnection _connection;

        static void Main(string[] args)
        {
            StartConnectionAsync().Wait();

            // React to 'ProcessMessage' messages from the Hub
            _connection.On<string>("process-message", (message) =>
            {
                Console.WriteLine(message);
            });

            _connection.On<SomeComplexType>("process-complex-type", (x) => {
                Console.WriteLine($"Received {x.Id} '{x.Name}'");
            });

            while (true)
            {
                try
                {
                    Console.Write(">");
                    var msg = Console.ReadLine();
                    if (msg == "quit")
                    {
                        break;
                    }

                    // There is no 'MadeUp' method on the hub. We're expecting an error here.
                    if (msg == "test")
                    {
                        _connection.InvokeAsync("MadeUp", msg);
                    }

                    // Grab a name and call the BroadcastComplexType method on the hub
                    if (msg == "complex-type")
                    {
                        Console.Write("Name: ");
                        var name = Console.ReadLine();
                        _connection.InvokeAsync("BroadcastComplexType", name);
                    }

                    if(msg == "pass-complex-type")
                    {
                        _connection.InvokeAsync("PassComplexType", new SomeComplexType());
                    }

                    // Call the Broadcast() method on the hub (which in turn calls 'process-message' on each client)
                    _connection.InvokeAsync("Broadcast", msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            _connection.DisposeAsync().Wait();
        }

        public static async Task StartConnectionAsync()
        {
            _connection = new HubConnectionBuilder()
                 .WithUrl("http://localhost:53681/myhub")
                 .WithConsoleLogger()
                 .Build();

            await _connection.StartAsync();
        }
    }
}
