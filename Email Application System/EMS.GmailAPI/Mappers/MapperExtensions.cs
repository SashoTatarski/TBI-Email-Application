using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.GmailAPI.gmail_Models;
using System;
using System.Linq;

namespace EMS.GmailAPI.Mappers
{
    internal static class MapperExtensions
    {
        internal static EmailDomain MapToDomainModel(this Email email)
        {
            return new EmailDomain
            {
                GmailMessageId = email.GmailMessageId,
                Received = email.Received,
                SenderEmail = email.SenderEmail,
                SenderName = email.SenderName,
                Subject = email.Subject,
                NumberOfAttachments = email.Attachments.Count,
                SizeOfAttachmentsMb = email.Attachments.Sum(att => att.SizeMb),
                Status = EmailStatus.NotReviewed,
                ToCurrentStatus = DateTime.UtcNow
            };
        }

        internal static AttachmentDomain MapToDomainModel(this Attachment attachment)
        {
            return new AttachmentDomain
            {
                Name = attachment.Name,
                SizeMb = attachment.SizeMb
            };
        }
    }
}
