using POC.API.IntegrationTests.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.API.IntegrationTests.Http.Clients
{
    public interface IPocHttpClient: IHttpClient
    {
        Task<HttpResponseMessage> CreateSubscriber(CreateSubscribeReq req, CancellationToken cancellationToken);
        Task<HttpResponseMessage> RetrieveSubscriberById(int id, CancellationToken cancellationToken);
    }
}
