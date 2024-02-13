using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.API.IntegrationTests.Http
{
    public class OauthCredentialValue
    {
        public const string OauthCredential = "OauthCredential";     
        public string? ClientSecret { get; set; }
       
    }
}
