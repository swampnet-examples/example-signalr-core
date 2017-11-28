using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Hubs;

namespace SignalRDemo.Services
{
    public class TestBackgroundService : BackgroundService
    {
        private readonly IHubContext<MyHub> _hub;

        public TestBackgroundService(IHubContext<MyHub> hub)
        {
            _hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _hub.Clients.All.InvokeAsync("process-message", DateTime.Now.ToString());
                await Task.Delay(10000);
            }

            await Task.CompletedTask;
        }
    }
}
