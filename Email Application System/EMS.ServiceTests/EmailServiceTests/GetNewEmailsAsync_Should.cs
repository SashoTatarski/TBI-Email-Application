using EMS.Data;
using EMS.Data.Enums;
using EMS.Services;
using EMS.Services.Tests;
using GmailAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.ServiceTests.EmailServiceTests
{
    [TestClass]
    public class GetNewEmailsAsync_Should
    {
        [TestMethod]
        public async Task GetNewEmails()
        {
            TestUtils.GetContextWithEmails(nameof(GetNewEmails));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetNewEmails))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var newEmails = await sut.GetEmailsAsync(EmailStatus.New);

                var expectedOpenEmailsCount = TestUtils.Emails.Where(mail => mail.Status == EmailStatus.Open).Count();

                var actualNewEmailsCount = newEmails.Count();

                Assert.AreEqual(expectedOpenEmailsCount, actualNewEmailsCount);

            }
        }

        [TestMethod]
        public async Task GetAllAttachmentsNewEmails()
        {
            TestUtils.GetContextWithEmails(nameof(GetAllAttachmentsNewEmails));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetAllAttachmentsNewEmails))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var openEmails = await sut.GetEmailsAsync(EmailStatus.New);

                int actualAttachmentsCount = 0;
                openEmails.ForEach(email =>
                actualAttachmentsCount += email.Attachments.Count);

                int expectedAttachmentsCount = 0;
                TestUtils.Emails
                    .Where(mail => mail.Status == EmailStatus.New).ToList()
                    .ForEach(email =>
                        expectedAttachmentsCount += email.Attachments.Count);

                Assert.AreEqual(expectedAttachmentsCount, actualAttachmentsCount);
            }
        }
    }
}
