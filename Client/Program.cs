using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	class Program
	{
        static HubConnection _hubConnection;

        static void Main(string[] args)
		{
            Console.Write("key to start");
            Console.ReadKey();
            Console.Clear();

            Boosh();

			Console.Write("waiting...");
			Console.ReadKey();
		}


		// https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/hubs-api-guide-net-client#clientsetup
		private static async Task Boosh()
		{
			try
			{
				_hubConnection = new HubConnection("http://localhost:53682/myhub");
                _hubConnection.TraceLevel = TraceLevels.All;
                _hubConnection.TraceWriter = Console.Out;
                //IHubProxy myHubProxy = _hubConnection.CreateHubProxy("myhub");

                //stockTickerHubProxy.On<Stock>("UpdateStockPrice", stock => Console.WriteLine("Stock update for {0} new price {1}", stock.Symbol, stock.Price));
                await _hubConnection.Start();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
    }
}
