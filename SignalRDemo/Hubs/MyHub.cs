using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Shared;
using SignalRDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRDemo.Hubs
{
	public class MyHub : Hub
	{
        private readonly IClients _clients;

        public MyHub(IClients clients)
        {
            _clients = clients;
        }


        /// <summary>
        /// Broadcast a message to all connected clients
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Task Broadcast(string text)
		{
            // 'process-message' is probably not a good name, I think SignalR spins up a dynamic object in js, and this makes for an illegal name (unless it does
            // some jiggery pokery under the hood)
			return Clients.All.InvokeAsync("process-message", text);
		}

        public Task BroadcastComplexType(string name)
        {
            return Clients.All.InvokeAsync("process-complex-type", new SomeComplexType() { Name = name });
        }

        public Task PassComplexType(SomeComplexType t)
        {
            return Clients.All.InvokeAsync("process-message", $"Received complex type {t.Id}");
        }

        public override Task OnConnectedAsync()
        {
            _clients.Add(Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _clients.Remove(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
