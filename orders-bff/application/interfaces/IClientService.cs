using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using application.dtos.request;
using application.models;

namespace application.interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> SearchClients(OrderSearchDto search);
        Task<Client> GetClient(Guid id);
        Task<Client> SaveClient(Client client);
    }
}