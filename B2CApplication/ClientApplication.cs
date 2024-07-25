using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2CApplication
{
    public sealed class B2CMsalClientApplication
    {
        private readonly IEnumerable<string> _scopes;
        private readonly IConfidentialClientApplication _cca;

        public IConfidentialClientApplication Application { get { return _cca; } }
        public IEnumerable<string> Scopes { get { return _scopes; } }

        private readonly static B2CMsalClientApplication instance = new B2CMsalClientApplication();

        static B2CMsalClientApplication() { }

        private B2CMsalClientApplication() {
            _scopes = new[] { "<api_scopes>" };
            _cca = ConfidentialClientApplicationBuilder.Create("<client_id>")
                    .WithB2CAuthority($"https://<tenant>.b2clogin.com/tfp/<tenant_id>/<sign_in_policy>")
                    .WithRedirectUri("https://localhost:44348/signin")
                    .WithClientSecret("<client_secret>")
                    .WithCacheOptions(CacheOptions.EnableSharedCacheOptions)
                    .Build();
        }

        public static B2CMsalClientApplication Instance
        {
            get
            {
                return instance;
            }
        }
    }
}