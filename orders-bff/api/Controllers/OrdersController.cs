using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application.dtos.request;
using application.dtos.response;
using application.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;

        public OrdersController(
            ILogger<OrdersController> logger,
            IClientService clientService,
            IOrderService orderService
        )
        {
            _logger = logger;
            _orderService = orderService;
        }

        /// <summary>
        /// Searches for orders along with resumed information about the client.
        /// </summary>
        /// <param name="name">The Client Name</param> 
        /// <param name="phone">The Client's Phone number</param> 
        /// <param name="email">The Client's E-mail</param> 
        /// <param name="startDate">The start date for the Creation Date of the Order</param> 
        /// <param name="endDate">The end date for the Creation Date of the Order</param> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Search(
            [FromQuery] String name,
            [FromQuery] String phone,
            [FromQuery] String email,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate
        )
        {

            var dto = new OrderSearchDto
            {
                ClientName = name,
                Email = email,
                Phone = phone,
                StartDate = startDate,
                EndDate = endDate
            };

            _logger.Log(LogLevel.Information, "Search", dto);

            return Ok(await _orderService.Search(dto));
        }

        /// <summary>
        /// Gets an Order with all the details about items and the client.
        /// </summary>
        /// <param name="id">The ID of the order</param> 
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderFullDto>> GetFullOrder([FromRoute] Guid id)
        {
            _logger.Log(LogLevel.Information, "Get Full Order", id);

            return Ok(await _orderService.GetOrder(id));
        }
    }
}
