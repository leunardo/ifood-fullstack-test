using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using application.dtos.request;
using application.dtos.response;
using application.models;

namespace application.interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> Search(OrderSearchDto search);
        Task<OrderFullDto> GetOrder(Guid id);
        Task<Order> SaveOrder(Order client);
        Task PopulateDatabases();
    }
}