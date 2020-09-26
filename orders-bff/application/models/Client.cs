using System;
using Newtonsoft.Json;

namespace application.models
{
    public class Client
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}