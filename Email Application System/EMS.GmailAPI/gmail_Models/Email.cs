using System;
using System.Collections.Generic;

namespace EMS.GmailAPI.gmail_Models
{
    internal class Email
    {
        internal string GmailMessageId { get; set; }
        internal DateTime Received { get; set; }
        internal string SenderEmail { get; set; }
        internal string SenderName { get; set; }
        internal string Subject { get; set; }
        internal string Body { get; set; }
        internal List<Attachment> Attachments { get; set; }
    }
}
