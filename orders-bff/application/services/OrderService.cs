using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application.dtos.request;
using application.dtos.response;
using application.helpers;
using Rng = application.helpers.Random;
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
            var orders = SearchOrders(search);
            var clients = _clientService.SearchClients(search);

            await Task.WhenAll(orders, clients);
            
            var result = orders.Result
                .Where(order => clients.Result.Any(client => client.Id == order.ClientId))
                .Select(
                    order => CreateOrderDto(order, clients.Result)
                );

            return result;
        }

        
        public async Task<OrderFullDto> GetOrder(Guid id)
        {
            var order = await FindOrder(id);
            var client = await _clientService.GetClient(order.ClientId);
            var result = CreateOrderFullDto(order, client);

            return result;
        }

        private async Task<Order> FindOrder(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                var request = await httpClient
                    .WithUrl(_apiUrl + $"/orders/{id}")
                    .GetAsync<JToken>();

                var order = _mapper.Map<Order>(request);
                return order;
            }
        }

        private async Task<IEnumerable<Order>> SearchOrders(OrderSearchDto search)
        {
            using (var httpClient = new HttpClient())
            {
                var request = await httpClient
                    .AddQueryParameter("start", search.StartDate.GetValueOrDefault(DateTime.MinValue).ToString(_dateFormat))
                    .AddQueryParameter("end", search.EndDate.GetValueOrDefault(DateTime.MaxValue).ToString(_dateFormat))
                    .WithUrl(_apiUrl + "/orders/search/byDate")
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

        private OrderFullDto CreateOrderFullDto(Order order, Client client)
        {
            return new OrderFullDto
            {
                Client = CreateClientDto(client),
                Id = order.Id,
                Items = _mapper.Map<List<ItemDto>>(order.Items),
                TotalValue = order.Items.Select(i => i.Price * i.Quantity).Sum()
            };
        }

        public async Task<Order> SaveOrder(Order order)
        {
            using (var httpClient = new HttpClient())
            {
                var json = await httpClient
                    .WithUrl(_apiUrl + $"/orders")
                    .PostAsync<JToken, Order>(order);

                var orderResult = _mapper.Map<Order>(json);

                return orderResult;
            }
        }

        public async Task PopulateDatabases()
        {
           var rng = Rng.rng;

           for (var i = 0; i < rng.Next(1, 10); i++)
           {
               var client = Rng.GenerateClient();
               client = await _clientService.SaveClient(client);

               for (var j = 0; j < rng.Next(1, 10); j++)
               {
                   var order = Rng.GenerateOrder(client);
                   await SaveOrder(order);
               }
           }
        }
    }
}