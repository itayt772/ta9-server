using System.Collections.Generic;
using System.Linq;
using ta9.Core.interfaces;
using ta9.Core.models;
using ta9.Models;

namespace ta9.Core
{
    public class ClientService : IClientService
    {
        private readonly IDataStorageService<List<Client>> _dataStorageService;
        private const string STORAGE_KEY = "clients_details";

        public ClientService(IDataStorageService<List<Client>> dataStorageService)
        {
            _dataStorageService = dataStorageService;
        }

        public void SubsriceClient(ClientDetails clientDetails, string connectionId)
        {
            var clients = GetAllClients();
            var client = clients.FirstOrDefault(x => x.ClientDetails.Ip == clientDetails.Ip);

            if (client == null)
            {
                // add new client
                client = new Client
                {
                    ClientDetails = clientDetails,
                    Connections = new List<Connection>()
                };
                clients.Add(client);
            }

            // add new connection
            if (client.Connections.Any(x => x.ConnectionID == connectionId))
            {
                client.Connections.First(x => x.ConnectionID == connectionId).Connected = true;
            }
            else
            {
                client.Connections.Add(new Connection()
                {
                    Connected = true,
                    ConnectionID = connectionId
                });
            }

            _dataStorageService.Save(STORAGE_KEY, clients);
        }

        public void DisconnectClient(string connectionId)
        {
            var clients = GetAllClients();

            var client = clients.FirstOrDefault(x => x.Connections.Any(c => c.ConnectionID == connectionId));

            if (client == null)
            {
                // log connection not found
                return;
            }

            var connection = client.Connections.FirstOrDefault(x => x.ConnectionID == connectionId);
            connection.Connected = false;
            _dataStorageService.Save(STORAGE_KEY, clients);
        }

        public List<Client> GetAllClients()
        {
            var clients = _dataStorageService.Get(STORAGE_KEY);
            return (clients == null) ? new List<Client>() : clients;
        }
    }
}