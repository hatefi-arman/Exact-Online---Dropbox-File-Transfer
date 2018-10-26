<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentAttachmentsEdit.aspx.cs"
    Inherits="FileTransfer.DocumentAttachmentsEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Exact Online REST API File Transfer Sample</title>
    <link rel="stylesheet" type="text/css" href="css/style.css" />
</head>
<body>
    <div class="header">
        <div class="developer-portal">
            <a id="developer-portal" href="https://start.exactonline.nl/docs/HlpDocument.aspx?Mode=0&HelpFile=DocPortalHome.EN.HLP">Developers portal</a>
        </div>
        <div class="logo">
        </div>
    </div>
    <div class="section-header">
        Exact Online REST API File Transfer Sample
    </div>
    <div class="form">
        <form id="form1" runat="server">
            <input id="Action" runat="server" type="hidden" />
            <input id="DocumentID" runat="server" type="hidden" />

            <div class="row">
                <span class="label">Dropbox Items</span>
                <asp:DropDownList ID="DropboxItems" runat="server" CssClass="DropDownList" DataTextField="FilePath" DataValueField="FilePath" />
                <asp:Button class="button" ID="SaveButton" runat="server" Text="Transfer" OnClientClick="SetAction('Transfer');" />
                <asp:Button class="button" ID="CloseButton" runat="server" Text="Close" OnClientClick="return GoBack();" />
            </div>

            <div class="separator">
                <hr />
            </div>

            <div class="list">
                <span class="list-title">Attachment list</span>
                <div class="list-title-separator">
                </div>
                <asp:GridView runat="server" ID="documentAttachmentsGrid" EmptyDataText="No data available."
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="File Name" DataField="FileName" />
                        <asp:BoundField HeaderText="File Size" DataField="FileSize" />
                        <asp:HyperLinkField Text="Download" DataNavigateUrlFields="Url" />
                    </Columns>
                </asp:GridView>
            </div>
        </form>
    </div>
    <script type="text/javascript">
        function SetAction(actionValue) {
            document.getElementById("Action").value = actionValue;
        }

        function GoBack() {
            window.history.back();
            return false;
        }
    </script>
</body>
</html>
