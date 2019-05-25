using System.Web.Http;
using ta9.Core.interfaces;
using ta9.Hubs;
using ta9.Models;

namespace ta9.Controllers
{
    public class ClientController : ApiControllerWithHub<ClientDetailsHub>
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public void SubsriceClient(ClientDetailsDto newClientDetails)
        {
            //Hub.Clients.All.addItem(newClientDetails);
        }
    }
}