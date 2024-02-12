using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.API.IntegrationTests.Http.Clients
{
    public record EndpointsOption
    {
        [Required]
        public Uri? BaseUrl { get; set; }
        public string? GetSubscriberByIdEndpoint { get; set; }
    }
}
