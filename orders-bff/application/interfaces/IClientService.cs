using System.Collections.Generic;
using System.Threading.Tasks;
using application.dtos;
using application.models;

namespace application.interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> SearchClients(OrderSearchDto search);
    }
}