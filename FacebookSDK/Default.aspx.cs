using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Dynamic;
using Facebook;

namespace FacebookSDK
{
    public partial class Default : System.Web.UI.Page
    {
        private const string AppId = "214155861931492";
        private const string ExtendedPermissions = "user_about_me,publish_stream";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            FacebookLogin fblog = new FacebookLogin(AppId, ExtendedPermissions);


            FacebookOAuthResult oauthResult;

            if (fblog._fb.TryParseOAuthCallbackUrl(fblog._loginUrl, out oauthResult))
            {
                // The url is the result of OAuth 2.0 authentication
                Response.Write(oauthResult.AccessToken);
            }
            else
            {
                // The url is NOT the result of OAuth 2.0 authentication.
                Response.Redirect(fblog._loginUrl.AbsoluteUri);
            }

            
            
        }

        private Uri GenerateLoginUrl(string appId, string extendedPermissions)
        {
            // for .net 3.5
            // var parameters = new Dictionary<string,object>
            // parameters["client_id"] = appId;
            dynamic parameters = new ExpandoObject();
            parameters.client_id = appId;
            parameters.redirect_uri = "https://apps.facebook.com/markakodtest/";

            // The requested response: an access token (token), an authorization code (code), or both (code token).
            parameters.response_type = "token";

            // list of additional display modes can be found at http://developers.facebook.com/docs/reference/dialogs/#display
            parameters.display = "popup";

            // add the 'scope' parameter only if we have extendedPermissions.
            if (!string.IsNullOrWhiteSpace(extendedPermissions))
                parameters.scope = extendedPermissions;

            // generate the login url
            var fb = new FacebookClient();
            return fb.GetLoginUrl(parameters);
        }
    }
}