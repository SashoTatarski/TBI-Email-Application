using EMS.Data;
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
    public class GetSingleEmailAsync_Should
    {
        [TestMethod]
        public async Task GetEmail()
        {
            TestUtils.GetContextWithEmails(nameof(GetEmail));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetEmail))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var id = TestUtils.Emails[0].Id;
                var email = await sut.GetSingleEmailAsync(id.ToString());

                Assert.AreEqual(id, email.Id);
            }
        }

        [TestMethod]
        public async Task GetAttachment()
        {
            TestUtils.GetContextWithEmails(nameof(GetAttachment));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetAttachment))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var id = TestUtils.Emails[0].Id;
                var email = await sut.GetSingleEmailAsync(id.ToString());

                var actualAttachmentCount = email.Attachments.Count();
                var expectedAttachmentCount = TestUtils.Emails[0].Attachments.Count();

                Assert.AreEqual(expectedAttachmentCount, actualAttachmentCount);
            }
        }
    }
}
