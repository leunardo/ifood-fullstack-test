using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using application.dtos;
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
        public OrderService(
            IConfiguration configuration,
            IMapper mapper
        ) {
            _mapper = mapper;
            _apiUrl = configuration.GetValue<string>("Order:ApiUrl");
        }

        public async Task<IEnumerable<OrderDto>> Search(OrderSearchDto search)
        {
            var orders = await GetOrders(search);
            return null;
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
            }
            return null;
        }

    }
}