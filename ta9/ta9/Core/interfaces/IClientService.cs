using System.Collections.Generic;
using ta9.Core.models;
using ta9.Models;

namespace ta9.Core.interfaces
{
    public interface IClientService
    {
        void SubsriceClient(ClientDetails clientDetails, string connectionId);
        List<Client> GetAllClients();
        void DisconnectClient(string connectionId);
    }
}