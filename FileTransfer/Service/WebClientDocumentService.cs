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

        public IEnumerable<Document> GetDisplayList()
        {
            return Client.For<Document>()
                .Select(
                    d => d.ID,
                    d => d.Subject,
                    d => d.TypeDescription,
                    d => d.Created)
                .Get();
        }
    }
}