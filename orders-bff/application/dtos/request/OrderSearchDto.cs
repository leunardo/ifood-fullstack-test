using System;
using System.Collections.Generic;

namespace application.dtos.request
{
    public class OrderSearchDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}