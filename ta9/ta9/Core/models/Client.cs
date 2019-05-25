using System.Collections.Generic;
using System.Linq;
using ta9.Core.models;

namespace ta9.Models
{
    public class Client
    {
        public ClientDetails ClientDetails { get; set; }
        public List<Connection> Connections { get; set; }
        public bool Connected
        {
            get
            {
                return Connections.Any(x=> x.Connected);
            }
        }
    }
}