using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileTransfer.Domain
{
    public class DropboxDownloadResult
    {
        public string FileName { get; internal set; }
        public ulong FileSize { get { return (ulong)(Content != null ? Content.Length : 0); } }
        public byte[] Content { get; internal set; }
    }
}