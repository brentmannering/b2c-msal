using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2CApplication
{
    public partial class _Default : Page
    {
        private readonly IEnumerable<string> _scopes = B2CMsalClientApplication.Instance.Scopes;
        private readonly IConfidentialClientApplication _cca = B2CMsalClientApplication.Instance.Application;

        protected async void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // check for a cookie with the accountid in it (this could be encryped if need be)
                // the cookie is set on the signin page
                var accountId = Request.Cookies["account"].Value;
                var account = await _cca.GetAccountAsync(accountId);
                if (account == null)
                {
                    // no account so redirect to the login page
                    var redirect = await _cca.GetAuthorizationRequestUrl(_scopes).ExecuteAsync();
                    Response.Redirect(redirect.ToString(), false);
                }
                else
                {
                    // we have an account so get the token and apply the claims principal to the current user context (note Owin middleware does this usually)
                    var tokenResult = await _cca.AcquireTokenSilent(_scopes, account).ExecuteAsync();
                    HttpContext.Current.User = tokenResult.ClaimsPrincipal;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}