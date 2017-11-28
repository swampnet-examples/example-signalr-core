using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Client.Core
{
    class Program
    {
        private static HubConnection _connection;

        static void Main(string[] args)
        {
            Console.WriteLine("key");
            Console.ReadKey();
            Console.Clear();

            StartConnectionAsync();

            _connection.On<string>("Boosh", (message) =>
            {
                Console.WriteLine(message);
            });

            Console.WriteLine("key");
            Console.ReadKey();
            
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
