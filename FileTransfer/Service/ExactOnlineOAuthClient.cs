using System;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using DotNetOpenAuth.OAuth2;
using FileTransfer.Core.Web;
using FileTransfer.Domain;

namespace FileTransfer.Service
{
    /// <summary>
    /// The customized OAuth implementation for Exact Online service provider.
    /// Each secure page contains an instance of this class.
    /// </summary>
    public class ExactOnlineOAuthClient : WebServerClient
    {
        #region Properties

        public IAuthorizationState Authorization { get; set; }

        #endregion

        #region Constructor

        public ExactOnlineOAuthClient()
            : base(CreateAuthorizationServerDescription(), ConfigurationHelper.ExactOnlineClientId, ConfigurationHelper.ExactOnlineClientSecret)
        {
            // Initialization is already done through the base constructor
            ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(ConfigurationHelper.ExactOnlineClientSecret);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handle OAuth2 authorization and store the authorization in the session so it's available on all our pages
        /// </summary>
        public void Authorize(HttpSessionState session, string relativeReturnUrl)
        {
            Authorization = (IAuthorizationState)session["Authorization"];
            Uri uri = generateAuthorizationReturnUrl(relativeReturnUrl);
            Authorize(uri);
            session["Authorization"] = Authorization;

            RetrieveCurrentCompany();
        }

        private static Uri generateAuthorizationReturnUrl(string relativeReturnUrl)
        {
            if (relativeReturnUrl.StartsWith("/"))
                relativeReturnUrl = relativeReturnUrl.Substring(1);
            
            return new Uri(System.IO.Path.Combine(GetUrlRoot() , relativeReturnUrl));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method takes care of getting and refreshing the access token 
        /// </summary>
        private void Authorize(Uri returnUri)
        {
            if (Authorization == null)
            {
                Authorization = ProcessUserAuthorization();
                if (Authorization == null)
                {
                    // Kick off authorization request
                    RequestUserAuthorization(null, returnUri);
                }
            }
            else
            {
                if (AccessTokenHasToBeRefreshed())
                {
                    RefreshAuthorization(Authorization);
                }
            }
        }

        /// <summary>
        /// Check if the access token is expired or will soon expire.
        /// </summary>
        private Boolean AccessTokenHasToBeRefreshed()
        {
            TimeSpan timeToExpire = Authorization.AccessTokenExpirationUtc.Value.Subtract(DateTime.UtcNow);

            return (timeToExpire.Minutes < 1);
        }

        //TODO: Should be removed due to simple implementation,
        private static string MyClientIdentifier()
        {
            return ConfigurationHelper.ExactOnlineClientId;
        }

        //TODO: Should be removed due to simple implementation,
        private static string MyClientSecret()
        {
            return ConfigurationHelper.ExactOnlineClientSecret;
        }

        private static AuthorizationServerDescription CreateAuthorizationServerDescription()
        {
            var exactOnlineUrl = ConfigurationHelper.ExactOnlineBaseUrl;

            var uri = new Uri(exactOnlineUrl.EndsWith("/") ? exactOnlineUrl : exactOnlineUrl + "/");

            var serverDescription = new AuthorizationServerDescription
            {
                AuthorizationEndpoint = new Uri(uri, "api/oauth2/auth"),
                TokenEndpoint = new Uri(uri, "api/oauth2/token")
            };

            return serverDescription;
        }

        private static string GetUrlRoot()
        {
            string port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            port = port == null || port == "80" || port == "443" ? "" : ":" + port;

            string protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            protocol = protocol == null || protocol == "0" ? "http://" : "https://";

            return protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + HttpContext.Current.Request.ApplicationPath;
        }

        private void RetrieveCurrentCompany()
        {
            if (Me.CurrentCompany == 0)
            {
                using (var meService = new WebClientMeService(Authorization))
                {
                    Me.CurrentCompany = meService.GetCurrentCompany();
                }
            }
        }

        #endregion
    }
}