using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ta9.Core.interfaces;
using ta9.Core.models;
using ta9.Models;

namespace ta9.Hubs
{
    [HubName("clientDetails")]
    public class ClientDetailsHub : Hub
    {
        private readonly IClientService _clientService;

        public ClientDetailsHub(IClientService clientService)
        {
            _clientService = clientService;
        }

        public void SubsriceClient(ClientDetailsDto newClientDetails)
        {
            _clientService.SubsriceClient(new ClientDetails()
            {
                Ip = newClientDetails.Ip
            },
            Context.ConnectionId);

            NotifyAllClients();
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _clientService.DisconnectClient(Context.ConnectionId);

            NotifyAllClients();
            return base.OnDisconnected(stopCalled);
        }

        private void NotifyAllClients()
        {
            Clients.All.notifyClients(_clientService.GetAllClients());
        }
    }
}