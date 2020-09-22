using System.Collections.Generic;
using System.Threading.Tasks;
using application.dtos;

namespace application.interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> Search(OrderSearchDto search);
    }
}