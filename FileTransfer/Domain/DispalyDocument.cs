using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileTransfer.Domain
{
    public class DispalyDocument
    {
        public Guid ID { get; set; }
        public DateTime? Created { get; set; }
        public string Subject { get; set; }
        public int AttachmentsCount { get; set; }
    }
}