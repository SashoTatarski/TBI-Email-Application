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
    public class GetBodyAsync_Should
    {
        [TestMethod]
        public async Task GetBody()
        {
            TestUtils.GetContextWithEmails(nameof(GetBody));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetBody))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                gmailServiceMock
                    .Setup(g => g.GetEmailBodyAsync("mockId"))
                    .ReturnsAsync("Test Body");
                var emailBody = await sut.GetBodyByGmailAsync("mockId");

                Assert.AreEqual("Test Body", emailBody);
            }
        }

    }
}
