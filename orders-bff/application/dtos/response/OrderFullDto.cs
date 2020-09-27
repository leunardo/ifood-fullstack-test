using System;
using System.Collections.Generic;

namespace application.dtos.response
{
    public class OrderFullDto
    {
        public Guid Id { get; set; }
        public ClientDto Client { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
        public double TotalValue { get; set; }
    }
}