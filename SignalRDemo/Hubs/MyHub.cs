using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
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


        public Task Broadcast(string text)
		{            
			return Clients.All.InvokeAsync("ProcessMessage", text);
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
