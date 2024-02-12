﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.API.IntegrationTests.Http.Clients
{
    public interface IPocHttpClient: IHttpClient
    {
        Task<HttpResponseMessage> CreateSubscriber();
        Task<HttpResponseMessage> RetrieveSubscriberById(CancellationToken cancellationToken);
    }
}
