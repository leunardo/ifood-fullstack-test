using System;

namespace application.dtos.response
{
    public class ItemDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Total
        {
            get
            {
                return this.Quantity * this.UnitPrice;
            }
        }
    }
}