using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace application.dtos.request
{
    public class ClientDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
    }

    public class ClientEmbedded 
    {
        public List<ClientDto> Clients {get; set; }
    }

}