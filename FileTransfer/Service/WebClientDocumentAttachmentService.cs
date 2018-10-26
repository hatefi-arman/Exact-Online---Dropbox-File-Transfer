using System;
using System.Collections.Generic;
using DotNetOpenAuth.OAuth2;
using ExactOnline.Client.Models.Documents;

namespace FileTransfer.Service
{
    public class WebClientDocumentAttachmentService : WebClientServiceBase
    {
        #region Constructor

        public WebClientDocumentAttachmentService(IAuthorizationState authorizationState) : base(authorizationState)
        {
        }

        #endregion

        public IEnumerable<DocumentAttachment> Get(Guid documentId)
        {
            return Client.For<DocumentAttachment>()
                .Where(da => da.Document, documentId)
                .Select(
                    d => d.ID,
                    d => d.Attachment,
                    d => d.FileName,
                    d => d.FileSize,
                    d => d.Url)
                .Get();
        }

        public int Count(Guid documentId)
        {
            return Client.For<DocumentAttachment>()
                .Where(da => da.Document, documentId)
                .Count();
        }

        public DocumentAttachment Add(Guid documentId, string fileName, byte[] content)
        {
            var attachment = new DocumentAttachment {
                Document = documentId,
                Attachment = content,
                FileName = fileName,
                FileSize = content.Length
            };

            var operationResult = Client.For<DocumentAttachment>().Insert(ref attachment);

            if (operationResult)
            {
                return attachment;
            }
            else
                throw new Exception("DocumentAttachment failed to create.");
        }
    }
}