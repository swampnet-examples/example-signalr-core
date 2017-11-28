using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Client.Core
{
    static class Program
    {
        private static HubConnection _connection;

        static void Main(string[] args)
        {
            Console.WriteLine("key");
            Console.ReadKey();
            Console.Clear();

            StartConnectionAsync().Wait();

            _connection.On<string>("ProcessMessage", (message) =>
            {
                Console.WriteLine(message);
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
                    if (msg == "test")
                    {
                        _connection.InvokeAsync("MadeUp", msg);
                    }

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
