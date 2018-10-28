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
        /// <summary>
        /// The Dropbox Api client proxy.
        /// </summary>
        private readonly DropboxClient _client;

        #region Constructor

        public DropboxClientService()
        {
            _client = new DropboxClient(ConfigurationHelper.DropboxAccessToken, new DropboxClientConfig("FileTransferDemo"));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the list of all the available files in the Dropbox account.
        /// </summary>
        /// <returns>List of all the available files in the Dropbox account in the form of current application domain object as DisplayDropboxItem.</returns>
        public async Task<IEnumerable<DisplayDropboxItem>> GetDropboxItems()
        {
            //Load recursively the list of available files in the root of Dropbox account and all the subsequent subdirectories.
            var dropboxItems = await _client.Files.ListFolderAsync("", recursive: true);

            return dropboxItems.Entries
                .Where(item => item.IsFile)  //Filter out the file items.
                .Select(item => new DisplayDropboxItem  //Convert Dropbox api metadata result to the local application domain object.
                    {
                        FilePath = item.PathDisplay
                    });
        }

        /// <summary>
        /// Downloads and returns the selected relative path to Dropbox item.
        /// </summary>
        /// <param name="path">The relative path to the file item in the Dropbox.</param>
        /// <returns>The download result in the form of DropboxDownloadResult as the current application domain object.</returns>
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