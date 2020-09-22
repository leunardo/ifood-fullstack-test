using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application.dtos.request;
using application.dtos.response;
using application.helpers;
using application.interfaces;
using application.models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace application.services
{
    public class OrderService : IOrderService
    {
        private readonly string _dateFormat = "yyyy-MM-ddTHH:mm:ss.fffZZZ";
        private readonly string _apiUrl;
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        public OrderService(
            IConfiguration configuration,
            IClientService clientService,
            IMapper mapper
        ) {
            _mapper = mapper;
            _apiUrl = configuration.GetValue<string>("Order:ApiUrl");
            _clientService = clientService;
        }

        public async Task<IEnumerable<OrderDto>> Search(OrderSearchDto search)
        {
            var orders = GetOrders(search);
            var clients = _clientService.SearchClients(search);

            await Task.WhenAll(orders, clients);
            
            var result = orders.Result.Select(
                order => CreateOrderDto(order, clients.Result)
            );

            return result;
        }

        private async Task<IEnumerable<Order>> GetOrders(OrderSearchDto search)
        {
            using (var httpClient = new HttpClient())
            {
                var request = await httpClient
                    .WithUrl(_apiUrl + "/orders/search/byDate")
                    .AddQueryParameter("start", search.StartDate.GetValueOrDefault(DateTime.MinValue).ToString(_dateFormat))
                    .AddQueryParameter("end", search.EndDate.GetValueOrDefault(DateTime.MaxValue).ToString(_dateFormat))
                    .GetAsync<JToken>();

                var orders = _mapper.Map<List<Order>>(request["_embedded"]["orders"]);

                return orders;
            }
        }

        private OrderDto CreateOrderDto(Order order, IEnumerable<Client> clients)
        {
            var client = clients.FirstOrDefault(client => client.Id == order.ClientId);
            return new OrderDto
            {
                Date = order.CreatedAt,
                Id = order.Id,
                TotalValue = order.Items.Sum(i => i.Price * i.Quantity),
                Client = CreateClientDto(client)
            };
        }

        private ClientDto CreateClientDto(Client client) => client != null
            ? _mapper.Map<ClientDto>(client)
            : null;
    }
}