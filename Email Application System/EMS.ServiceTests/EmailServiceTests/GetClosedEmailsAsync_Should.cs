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
    public class GetClosedEmailsAsync_Should
    {

        [TestMethod]
        public async Task GetClosedEmails()
        {
            TestUtils.GetContextWithEmails(nameof(GetClosedEmails));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetClosedEmails))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var closedEmails = await sut.GetEmailsAsync(EmailStatus.Closed);
                var actualOpenEmailsCount = closedEmails.Count();

                var expectedOpenEmailsCount = TestUtils.Emails.Where(mail => mail.Status == EmailStatus.Closed).Count();

                Assert.AreEqual(expectedOpenEmailsCount, actualOpenEmailsCount);
            }
        }

        [TestMethod]
        public async Task GetAllAttachmentsClosedEmails()
        {
            TestUtils.GetContextWithEmails(nameof(GetAllAttachmentsClosedEmails));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetAllAttachmentsClosedEmails))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var openEmails = await sut.GetEmailsAsync(EmailStatus.Closed);

                int actualAttachmentsCount = 0;
                openEmails.ForEach(email =>
                actualAttachmentsCount += email.Attachments.Count);

                int expectedAttachmentsCount = 0;
                TestUtils.Emails
                    .Where(mail => mail.Status == EmailStatus.Closed).ToList()
                    .ForEach(email =>
                        expectedAttachmentsCount += email.Attachments.Count);

                Assert.AreEqual(expectedAttachmentsCount, actualAttachmentsCount);
            }
        }

    }
}
