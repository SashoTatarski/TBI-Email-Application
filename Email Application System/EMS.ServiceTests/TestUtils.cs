using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EMS.Services.Tests
{
    public static class TestUtils
    {
        public static List<EmailDomain> Emails = new List<EmailDomain>()
        {
            new EmailDomain()
            {
                Id = Guid.NewGuid(),
                Received = DateTime.UtcNow,
                NumberOfAttachments = 2,
                GmailMessageId = "GmailMessageId_1",
                SenderEmail = "email1@ems.com",
                SenderName = "TestName_1",
                Subject = "TestSubject_1",
                Status = EmailStatus.New,
                ToCurrentStatus = DateTime.UtcNow.AddDays(1),
                Body = null,
                Attachments = new List<AttachmentDomain>
                {
                    new AttachmentDomain()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Attachment_1.0",
                        SizeMb = 10.10
                    },
                    new AttachmentDomain()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Attachment_1.1",
                        SizeMb = 10.11
                    }
                }
            },
            new EmailDomain()
            {
                Id = Guid.NewGuid(),
                Received = DateTime.UtcNow,
                NumberOfAttachments = 0,
                GmailMessageId = "GmailMessageId_2",
                SenderEmail = "email2@ems.com",
                SenderName = "TestName_2",
                Subject = "TestSubject_2",
                Status = EmailStatus.Open,
                ToCurrentStatus = DateTime.UtcNow.AddDays(2),
                Body = "Body_2",
                Attachments = new List<AttachmentDomain>()
            },
            new EmailDomain()
            {
                Id = Guid.NewGuid(),
                Received = DateTime.UtcNow,
                NumberOfAttachments = 3,
                GmailMessageId = "GmailMessageId_3",
                SenderEmail = "email3@ems.com",
                SenderName = "TestName_3",
                Subject = "TestSubject_3",
                Status = EmailStatus.Closed,
                ToCurrentStatus = DateTime.UtcNow.AddDays(3),
                Body = "Body_3",
                Attachments = new List<AttachmentDomain>
                {
                    new AttachmentDomain()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Attachment_3.0",
                        SizeMb = 30.10
                    },
                    new AttachmentDomain()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Attachment_3.1",
                        SizeMb = 30.11
                    },
                    new AttachmentDomain()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Attachment_3.2",
                        SizeMb = 30.12
                    }
                }
            }
        };

        public static List<ApplicationDomain> Applications = new List<ApplicationDomain>()
        {
            new ApplicationDomain()
            {
                Id = Guid.NewGuid(),
                EmailId = Guid.NewGuid(),
                UserId = Guid.NewGuid().ToString(),
                EGN = "1111111111",
                Name = "TestName_1",
                PhoneNumber = "+111111111111",
                Status = ApplicationStatus.NotReviewed,
                User = new UserDomain()
                {
                    UserName = "TestUserName_1"
                }
            },
             new ApplicationDomain()
            {
                Id = Guid.NewGuid(),
                EGN = "2222222222",
                Name = "TestName_2",
                PhoneNumber = "+222222222222",
                Status = ApplicationStatus.Approved
            },
            new ApplicationDomain()
            {
                Id = Guid.NewGuid(),
                EGN = "3333333333",
                Name = "TestName_3",
                PhoneNumber = "+333333333333",
                Status = ApplicationStatus.Rejected
            }
        };
        public static DbContextOptions<SystemDataContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<SystemDataContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        public static SystemDataContext GetContextWithEmails(string databaseName)
        {
            var options = GetOptions(databaseName);
            var context = new SystemDataContext(options);

            context.Emails.AddRange(Emails);
            context.SaveChanges();

            return context;
        }

        public static SystemDataContext GetContextWithApplications(string databaseName)
        {
            var options = GetOptions(databaseName);
            var context = new SystemDataContext(options);

            context.Applications.AddRange(Applications);
            context.SaveChanges();

            return context;
        }
    }
}
