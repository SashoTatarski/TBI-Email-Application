using EMS.Data;
using EMS.Services;
using EMS.Services.Tests;
using GmailAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace EMS.ServiceTests.EmailServiceTests
{
    [TestClass]
    public class GetGmailIdAsync_Should
    {
        [TestMethod]
        public async Task GetGmailId()
        {
            TestUtils.GetContextWithEmails(nameof(GetGmailId));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetGmailId))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var id = TestUtils.Emails[0].Id;
                var gmailMessageId = await sut.GetGmailIdAsync(id.ToString());

                Assert.AreEqual(TestUtils.Emails[0].GmailMessageId, gmailMessageId);
            }
        }
    }
}
