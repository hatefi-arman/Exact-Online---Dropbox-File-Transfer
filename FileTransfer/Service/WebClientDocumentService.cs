using System.Collections.Generic;
using DotNetOpenAuth.OAuth2;
using ExactOnline.Client.Models.Documents;

namespace FileTransfer.Service
{
    public class WebClientDocumentService : WebClientServiceBase
    {
        #region Constructor

        public WebClientDocumentService(IAuthorizationState authorizationState) : base(authorizationState)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the list of the available Documents of the current user account at ExactOnline.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Document> GetDisplayList()
        {
            return Client.For<Document>()
                .Select(    //Chooses the required fields of the Documents to be dispalyed in the screen.
                    d => d.ID,
                    d => d.Subject,
                    d => d.TypeDescription,
                    d => d.Created)
                .Get();
        } 

        #endregion
    }
}