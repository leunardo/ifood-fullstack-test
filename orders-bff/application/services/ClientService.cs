using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using application.dtos;
using application.dtos.request;
using application.helpers;
using application.interfaces;
using application.models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace application.services
{
    public class ClientService : IClientService
    {
        private readonly string _apiUrl;
        private readonly IMapper _mapper;
        public ClientService(
            IConfiguration configuration,
            IMapper mapper
        )
        {
            _apiUrl = configuration.GetValue<string>("Client:ApiUrl");
            _mapper = mapper;
        }

        public Task<Client> GetClient(Guid id)
        {
            return FindClient(id);
        }

        private async Task<Client> FindClient(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                var json = await httpClient
                    .WithUrl(_apiUrl + $"/clients/{id}")
                    .GetAsync<JToken>();

                var client = _mapper.Map<Client>(json);

                return client;
            }
        }

        public async Task<IEnumerable<Client>> SearchClients(OrderSearchDto search)
        {
            using (var httpClient = new HttpClient())
            {
                var json = await httpClient
                    .AddQueryParameter("name", search.ClientName)
                    .AddQueryParameter("phone", search.Phone)
                    .AddQueryParameter("email", search.Email)
                    .WithUrl(_apiUrl + $"/clients/search/findAllClients")
                    .GetAsync<JToken>();

                var clients = _mapper.Map<List<Client>>(json["_embedded"]["clients"]);

                return clients;
            }
        }

        public async Task<Client> SaveClient(Client client)
        {
            using (var httpClient = new HttpClient())
            {
                var json = await httpClient
                    .WithUrl(_apiUrl + $"/clients")
                    .PostAsync<JToken, Client>(client);

                var clientResult = _mapper.Map<Client>(json);

                return clientResult;
            }
        }
    }
}