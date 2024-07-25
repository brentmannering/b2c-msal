using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2CApplication
{
    

    public partial class Signin : Page
    {
        private readonly IEnumerable<string> _scopes = B2CMsalClientApplication.Instance.Scopes;
        private readonly IConfidentialClientApplication _cca = B2CMsalClientApplication.Instance.Application;

        protected async void Page_Load(object sender, EventArgs e)
        {
            // the signin redirect passes exchange code on successful login, we swap the code for token (which gets added to cache automatically)
            var authCode = Request.Params.Get("code");
            var codeResult = await _cca.AcquireTokenByAuthorizationCode(_scopes, authCode).ExecuteAsync();

            // set the "login" cookie to keep track of everything (this could be set in session as well)
            Response.SetCookie(new HttpCookie("account", codeResult.Account.HomeAccountId.Identifier));
            Response.Redirect("/", false);            
        }
    }
}