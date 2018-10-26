using System;
using System.Web;
using FileTransfer.Core.Web;
using FileTransfer.Domain;
using FileTransfer.Service;

namespace FileTransfer
{
    public partial class DocumentAttachmentsEdit : SecurePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AuthorizeClient())
            {
                if (!IsPostBack)
                {
                    GetUrlParameters();
                }

                ExecuteAction();
            }
        }

        private void GetUrlParameters()
        {
            Action.Value = HttpContext.Current.Request.QueryString["Action"];
            DocumentID.Value = HttpContext.Current.Request.QueryString["DocumentID"];
        }

        private void ExecuteAction()
        {
            using (var documentAttachmentService = new WebClientDocumentAttachmentService(OAuthClient.Authorization))
            using (var dropboxService = new DropboxClientService())
            {
                switch (Action.Value)
                {
                    case "Load":
                        LoadAttachments(documentAttachmentService);
                        LoadDropboxItenns(dropboxService);
                        break;

                    case "Transfer":
                        if (String.IsNullOrWhiteSpace(DocumentID.Value))
                        {
                            NavigateToDocumentList();
                        }
                        else
                        {
                            TransferFile(dropboxService, documentAttachmentService);
                        }
                        break;
                }
            }
        }

        private void TransferFile(DropboxClientService dropboxService, WebClientDocumentAttachmentService documentAttachmentService)
        {
            var dropboxFile = dropboxService.GetFileContent(DropboxItems.SelectedValue).Result;

            documentAttachmentService.Add(new Guid(DocumentID.Value), dropboxFile.FileName, dropboxFile.Content);

            NavigateToDocumentList();
        }

        private void LoadDropboxItenns(DropboxClientService dropboxService)
        {
            DropboxItems.DataSource = dropboxService.GetDropboxItems().Result;
            DropboxItems.DataBind();
        }

        private void LoadAttachments(WebClientDocumentAttachmentService service)
        {
            var documentId = new Guid(DocumentID.Value);
            var list = service.Get(documentId);

            documentAttachmentsGrid.DataSource = list;
            documentAttachmentsGrid.DataBind();
        }

        private void NavigateToDocumentList()
        {
            Server.Transfer("~/DocumentList.aspx");
        }

    }
}