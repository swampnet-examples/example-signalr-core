using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRDemo.Services
{
    public interface IClients
    {
        void Add(string connectionId);
        void Remove(string connectionId);
        IEnumerable<string> Connections { get; }
    }


    public class Clients : IClients
    {
        private readonly List<string> _connections = new List<string>();

        public IEnumerable<string> Connections => _connections;


        public void Add(string connectionId)
        {
            lock (_connections)
            {
                _connections.Add(connectionId);
            }
        }


        public void Remove(string connectionId)
        {
            lock (_connections)
            {
                _connections.Remove(connectionId);
            }
        }
    }
}
