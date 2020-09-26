using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace application.models
{
    public class Order
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        
        [JsonProperty("clientId")]
        public Guid ClientId { get; set; }

        [JsonProperty("restaurantId")]
        public Guid RestaurantId { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("confirmedAt")]
        public DateTime ConfirmedAt { get; set; }

        [JsonProperty("items")]
        public IEnumerable<Item> Items { get; set; }
    }
}