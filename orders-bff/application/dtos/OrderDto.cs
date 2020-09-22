using System;

namespace application.dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TotalValue { get; set; }
    }
}