using System;
using System.Collections.Generic;
using System.Net;
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

        #region Public Methods

        /// <summary>
        /// Returns the list of Attachments of the given Document ID.
        /// </summary>
        /// <param name="documentId">The Document ID for which to find the Attachments.</param>
        /// <returns>The list of DocumentAttachment</returns>
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

        /// <summary>
        /// Returns the number of attachments of the given Document ID.
        /// </summary>
        /// <param name="documentId">The Document ID for which to find the number of Attachments.</param>
        /// <returns>The number of attachments</returns>
        public int Count(Guid documentId)
        {
            return Client.For<DocumentAttachment>()
                .Where(da => da.Document, documentId)
                .Count();
        }

        /// <summary>
        /// Creates a DocumentAttachment with the provided binary content and adds it the existing attchments of the given Document ID.
        /// </summary>
        /// <param name="documentId">The Document ID for which to add the Attachment to it.</param>
        /// <param name="fileName">The name to be disolayed as the original File name.</param>
        /// <param name="content">The binary data representing the content of uploaded file as the attachment.</param>
        /// <returns>The created DocumentAttachment.</returns>
        public DocumentAttachment Add(Guid documentId, string fileName, byte[] content)
        {
            var attachment = new DocumentAttachment
            {
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

        #endregion
    }
}