using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using DotNetOpenAuth.OAuth2;

namespace FileTransfer.Service
{
    public class WebClientMeService : WebClientServiceBase
    {
        #region Constructor

        public WebClientMeService(IAuthorizationState authorizationState)
            : base(authorizationState)
        {
        }

        #endregion

        #region Public Methods

        public int GetCurrentCompany()
        {
            return Client.CurrentMe().CurrentDivision;
        }

        #endregion
    }
}