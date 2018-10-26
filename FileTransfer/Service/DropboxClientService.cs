using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetOpenAuth.OAuth2;
using Dropbox.Api;
using Dropbox.Api.Files;
using ExactOnline.Client.Models.Documents;
using FileTransfer.Core.Web;
using FileTransfer.Domain;

namespace FileTransfer.Service
{
    public class DropboxClientService : IDisposable
    {
        private readonly DropboxClient _client;

        //private readonly DropboxAppClient _appClient;

        #region Constructor

        public DropboxClientService()
        {
            _client = new DropboxClient(ConfigurationHelper.DropboxAccessToken, new DropboxClientConfig("FileTransferDemo"));

            //_appClient = new DropboxAppClient(ConfigurationHelper.DropboxAppKey, ConfigurationHelper.DropboxAppSecret);
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<DisplayDropboxItem>> GetDropboxItems()
        {
            var dropboxItems = await _client.Files.ListFolderAsync("", recursive: true);

            return dropboxItems.Entries.Where(item => item.IsFile).Select(item => new DisplayDropboxItem
            {
                FilePath = item.PathDisplay
            });
        }

        public async Task<DropboxDownloadResult> GetFileContent(string path)
        {
            var downloadResponse = await _client.Files.DownloadAsync(path);

            var result = new DropboxDownloadResult {
                FileName = downloadResponse.Response.AsFile.Name,
                Content = await downloadResponse.GetContentAsByteArrayAsync()
        };
            return result;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                if (_client != null)
                {
                    _client.Dispose();
                }
            }
        }

        #endregion
    }
}