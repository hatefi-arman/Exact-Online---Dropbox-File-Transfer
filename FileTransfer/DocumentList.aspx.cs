using System;
using System.Collections.Generic;
using System.Linq;
using ExactOnline.Client.Models.Documents;
using FileTransfer.Core.Web;
using FileTransfer.Domain;
using FileTransfer.Service;

namespace FileTransfer
{
    public partial class DocumentList : SecurePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AuthorizeClient())
            {
                InitializeGrid();
            }
        }

        private void InitializeGrid()
        {
            using (var documentService = new WebClientDocumentService(OAuthClient.Authorization))
            {
                var documents = documentService.GetDisplayList();

                var dataList = new List<DispalyDocument>();

                using (var attachmentService = new WebClientDocumentAttachmentService(OAuthClient.Authorization))
                {
                    foreach (var document in documents)
                    {
                        var attachmentsCount = attachmentService.Count(document.ID);

                        dataList.Add(new DispalyDocument
                        {
                            ID = document.ID,
                            Subject = document.Subject,
                            Created = document.Created,
                            AttachmentsCount = attachmentsCount
                        });
                    }
                }

                documentsGrid.DataSource = dataList;
            }

            documentsGrid.DataBind();
        }
    }
}