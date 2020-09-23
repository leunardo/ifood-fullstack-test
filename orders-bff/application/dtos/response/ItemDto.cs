using System;

namespace application.dtos.response
{
    public class ItemDto
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total
        {
            get
            {
                return this.Quantity * this.Price;
            }
        }
    }
}