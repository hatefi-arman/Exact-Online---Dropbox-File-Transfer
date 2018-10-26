using DotNetOpenAuth.OAuth2;
using FileTransfer.Service;

namespace FileTransfer.Core.Web
{
    /// <summary>
    /// This is the base page for all the pages that require OAuth authorization from the service provider.
    /// </summary>
    public class SecurePageBase : System.Web.UI.Page
    {
        protected static readonly ExactOnlineOAuthClient OAuthClient = new ExactOnlineOAuthClient();

        public IAuthorizationState AuthorizationState
        {
            get { return OAuthClient.Authorization; }
        }

        protected bool AuthorizeClient()
        {
            OAuthClient.Authorize(Session, Request.CurrentExecutionFilePath);

            return (OAuthClient.Authorization != null);
        }
    }
}