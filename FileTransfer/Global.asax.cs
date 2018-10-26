using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace FileTransfer
{
    public class Global : System.Web.HttpApplication
    {
        protected void Session_Start(object sender, EventArgs e)
        {
            // Initialize a session object so the session remains persistent. 
            // Otherwise DotNetOpenAuth will throw a Protocolexception on ProcessUserAuthorization.
            // http://stackoverflow.com/questions/2874078/asp-net-session-sessionid-changes-between-requests

            Session["Init"] = 0;
        }
    }
}