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
    public class GetOpenEmailsAsync_Should
    {

        [TestMethod]
        public async Task GetOpenEmails()
        {
            TestUtils.GetContextWithEmails(nameof(GetOpenEmails));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetOpenEmails))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var openEmails = await sut.GetEmailsAsync(EmailStatus.Open);

                var expectedOpenEmailsCount = TestUtils.Emails.Where(mail => mail.Status == EmailStatus.Open).Count();

                var actualOpenEmailsCount = openEmails.Count();

                Assert.AreEqual(expectedOpenEmailsCount, actualOpenEmailsCount);
            }
        }

        [TestMethod]
        public async Task GetAllAttachmentsOpenEmails()
        {
            TestUtils.GetContextWithEmails(nameof(GetAllAttachmentsOpenEmails));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetAllAttachmentsOpenEmails))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var openEmails = await sut.GetEmailsAsync(EmailStatus.Open);

                int actualAttachmentsCount = 0;
                openEmails.ForEach(email =>
                actualAttachmentsCount += email.Attachments.Count);

                int expectedAttachmentsCount = 0;
                TestUtils.Emails
                    .Where(mail => mail.Status == EmailStatus.Open).ToList()
                    .ForEach(email =>
                        expectedAttachmentsCount += email.Attachments.Count);

                Assert.AreEqual(expectedAttachmentsCount, actualAttachmentsCount);
            }
        }

    }
}
