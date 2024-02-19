using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.API.IntegrationTests.Models.Responses
{
    public class Subscriber
    {
        public string? service { get; set; }
        public int id { get; set; }
        public string? email { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? date_subscribed { get; set; }
    }

}
