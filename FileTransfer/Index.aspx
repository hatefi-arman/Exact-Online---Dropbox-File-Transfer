<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FileTransfer.Start" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>File Transfer Sample</title>
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
        File Transfer Demo
    </div>
    <div class="form">
        <form id="form1" runat="server">
            <input id="Action" runat="server" type="hidden" />
            <fieldset>
                <legend class="LegendTitle">Exact Online Services Configuration</legend>

                <div class="URI">
                    <b>NOTE:</b> Please register an app in the Exact Online App Center.
                <br />
                    - use current URL as callback URL in the App Center<br />
                    - then fill in here your ClientID / ClientSecret
                <br />
                </div>
                <div class="row">
                    <span class="label">Client ID</span>
                    <asp:TextBox ID="ExactOnlineClientId" Width="290" runat="server" />
                </div>
                <div class="row">
                    <span class="label">Client Secret</span>
                    <asp:TextBox ID="ExactOnlineClientSecret" Width="290" runat="server" />
                </div>
                <br />
                <div class="URI">
                    Please select the Exact Online server
                </div>
                <div>
                    <span class="label">Exact Online Url</span>
                    <asp:DropDownList ID="ExactOnlineUrl" Width="295" runat="server">
                        <asp:ListItem Text="https://start.exactonline.nl" Value="https://start.exactonline.nl"></asp:ListItem>
                        <asp:ListItem Text="https://start.exactonline.be" Value="https://start.exactonline.be"></asp:ListItem>
                        <asp:ListItem Text="https://start.exactonline.co.uk" Value="https://start.exactonline.co.uk" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </fieldset>
            <br />
            <fieldset>
                <legend class="LegendTitle">Dropbox Configuration</legend>
                <div class="URI">
                    <b>NOTE:</b> Please register an app in your desired Dropbox account and fill in the folowing fields accordingly.
                <br />
                </div>
                <div class="row">
                    <span class="label">App Key</span>
                    <asp:TextBox ID="DropboxAppKey" Width="290" runat="server" />
                </div>
                <div class="row">
                    <span class="label">App Secret</span>
                    <asp:TextBox ID="DropboxAppSecret" Width="290" runat="server" />
                </div>
                <div class="row">
                    <span class="label">Access Token</span>
                    <asp:TextBox ID="DropboxAccessToken" Width="290" runat="server" />
                </div>

            </fieldset>
            <br />
            <div class="URI">
                When you have changed the configuration, then please <b>save</b> this first
                <br />
                - values are saved in AppSettings of the <b>Web.Config</b>
            </div>
            <br />
            <asp:Button ID="SaveButton" class="button" EnableThemingID="StartButton" runat="server"
                Text="Save the Configuration" OnClick="SaveButton_Click" />
            <asp:Button ID="StartButton" class="button" EnableThemingID="StartButton" runat="server"
                Text="Start" OnClick="StartButton_Click" />
        </form>
    </div>
    <script type="text/javascript">
        function GoBack() {
            window.history.back();
            return false;
        }
    </script>
</body>
</html>
