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
		static void Main(string[] args)
		{
			Boosh();

			Console.Write("waiting...");
			Console.ReadKey();
		}

		// https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/hubs-api-guide-net-client#clientsetup
		private static async Task Boosh()
		{

			Console.Write("key");
			Console.ReadKey();

			try
			{
				var hubConnection = new HubConnection("http://localhost:53682");
				IHubProxy stockTickerHubProxy = hubConnection.CreateHubProxy("my-hub");

				//stockTickerHubProxy.On<Stock>("UpdateStockPrice", stock => Console.WriteLine("Stock update for {0} new price {1}", stock.Symbol, stock.Price));
				await hubConnection.Start();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

		}
	}
}
