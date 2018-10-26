using System;
using System.Web.UI;
using System.Web.Configuration;
using FileTransfer.Core.Web;

namespace FileTransfer
{
    public partial class Start : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExactOnlineClientId.Text = ConfigurationHelper.ExactOnlineClientId;
                ExactOnlineClientSecret.Text = ConfigurationHelper.ExactOnlineClientSecret;
                ExactOnlineUrl.SelectedValue = ConfigurationHelper.ExactOnlineBaseUrl;

                DropboxAccessToken.Text = ConfigurationHelper.DropboxAccessToken;
                DropboxAppKey.Text = ConfigurationHelper.DropboxAppKey;
                DropboxAppSecret.Text = ConfigurationHelper.DropboxAppSecret;
            }
        }

        protected void StartButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/DocumentList.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            ConfigurationHelper.UpdateSettings(
                ExactOnlineClientId.Text,
                ExactOnlineClientSecret.Text,
                ExactOnlineUrl.SelectedValue,
                DropboxAppKey.Text,
                DropboxAppSecret.Text,
                DropboxAccessToken.Text
            );
        }

    }
}
