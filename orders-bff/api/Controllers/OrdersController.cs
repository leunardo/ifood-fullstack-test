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
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;

        public OrdersController(
            ILogger<OrdersController> logger,
            IClientService clientService,
            IOrderService orderService
        )
        {
            _logger = logger;
            _clientService = clientService;
            _orderService = orderService;
        }

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

        [HttpGet("{id}")]
        public ActionResult<OrderFullDto> GetFullOrder()
        {
            return Ok(null);
        }
    }
}
