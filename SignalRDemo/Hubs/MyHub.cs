using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRDemo.Hubs
{
	public class MyHub : Hub
	{
		public Task Boosh(string text)
		{
			return Clients.All.InvokeAsync("OnBoosh", text);
		}
	}
}
