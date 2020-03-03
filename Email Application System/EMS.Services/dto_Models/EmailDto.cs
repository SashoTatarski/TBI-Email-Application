using EMS.Data.Enums;
using System;
using System.Collections.Generic;

namespace EMS.Services.dto_Models
{
    public class EmailDto
    {
        public Guid Id { get; set; }
        public string GmailMessageId { get; set; }
        public DateTime Received { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailStatus Status { get; set; }
        public DateTime? ToNewStatus { get; set; }
        public DateTime? ToCurrentStatus { get; set; }
        public DateTime? ToTerminalStatus { get; set; }
        public int? NumberOfAttachments { get; set; }
        public double? SizeOfAttachmentsMb { get; set; }
        public ApplicationDto Application { get; set; }
        public List<AttachmentDto> Attachments { get; set; }       
    }
}
