using System;

namespace application.dtos.response
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public ClientDto Client { get; set; }
        public double TotalValue { get; set; }
    }
}