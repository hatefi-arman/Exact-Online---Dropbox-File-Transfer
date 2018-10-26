<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentList.aspx.cs"
    Inherits="FileTransfer.DocumentList" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Exact Online REST API Documents Sample</title>
    <link rel="stylesheet" type="text/css" href="css/style.css" />
</head>
<body>
    <div class="header">
        <div class="developer-portal">
            <a id="developer-portal" href="https://start.exactonline.nl/docs/HlpDocument.aspx?Mode=0&HelpFile=DocPortalHome.EN.HLP">Developers portal
            </a>
        </div>
        <div class="logo">
        </div>
    </div>
    <div class="section-header">
        Exact Online - Dropbox File Transfer
    </div>
    <div class="form">
        <form id="form1" runat="server" defaultbutton="RefreshButton">
            <asp:Button class="button" ID="RefreshButton" runat="server" Text="Refresh" />

            <div class="separator">
                <hr />
            </div>

            <div class="list">
                <span class="list-title">Document list</span>
                <div class="list-title-separator">
                </div>
                <asp:GridView runat="server" ID="documentsGrid" EmptyDataText="No data available."
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:HyperLinkField HeaderText="Action" Text="Add Attachment From Dropbox" DataNavigateUrlFields="ID"
                            DataNavigateUrlFormatString="~/DocumentAttachmentsEdit.aspx?Action=Load&DocumentID={0}" />
                        <asp:BoundField HeaderText="Subject" DataField="Subject" />
                        <asp:BoundField HeaderText="Attachments Count" DataField="AttachmentsCount" ItemStyle-Font-Bold="true" />
                        <asp:BoundField HeaderText="Created" DataField="Created" />
                    </Columns>
                </asp:GridView>
            </div>
        </form>
    </div>
</body>
</html>
