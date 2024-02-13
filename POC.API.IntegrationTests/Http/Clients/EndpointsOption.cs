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
        /* POC */
        public string? GetSubscriberById { get; set; }
        /* POC */

        /* OAUTH */
        public string? ObtainToken { get; set; }
        /* OAUTH */
    }
}
