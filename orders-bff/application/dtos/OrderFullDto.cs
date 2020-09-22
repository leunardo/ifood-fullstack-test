using System;
using System.Collections.Generic;

namespace application.dtos
{
    public class OrderFullDto
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
    }
}