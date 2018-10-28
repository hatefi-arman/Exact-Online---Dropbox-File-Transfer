using DotNetOpenAuth.OAuth2;
using FileTransfer.Service;

namespace FileTransfer.Core.Web
{
    /// <summary>
    /// This is the base page for all the pages that require OAuth authorization from the service provider.
    /// </summary>
    public class SecurePageBase : System.Web.UI.Page
    {
        /// <summary>
        /// Manages the application authorization using OAuth and provides AccessToken to the ExactOnline api service calls.
        /// </summary>
        protected static readonly ExactOnlineOAuthClient OAuthClient = new ExactOnlineOAuthClient();

        /// <summary>
        /// Exposes the Authorization State taken from OAuth library to provide AccessToken to the client.
        /// </summary>
        public IAuthorizationState AuthorizationState
        {
            get { return OAuthClient.Authorization; }
        }

        //Triggers the OAuth authorization at each page reuest.
        protected bool AuthorizeClient()
        {
            OAuthClient.Authorize(Session, Request.CurrentExecutionFilePath);

            return (OAuthClient.Authorization != null);
        }
    }
}