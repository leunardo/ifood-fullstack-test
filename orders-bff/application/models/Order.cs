using System;
using System.Collections;
using System.Collections.Generic;

namespace application.models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid RestaurantId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ConfirmedAt { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}