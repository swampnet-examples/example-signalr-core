using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Hubs;
using SignalRDemo.Services;

namespace SignalRDemo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IHubContext<MyHub> _hub;
        private readonly IClients _clients;

        public ValuesController(IHubContext<MyHub> hub, IClients clients)
        {
            _hub = hub;
            _clients = clients;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _clients.Connections;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            // HACK: Broadcast
            _hub.Clients.All.InvokeAsync("Boosh", DateTime.Now.ToString());

            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
