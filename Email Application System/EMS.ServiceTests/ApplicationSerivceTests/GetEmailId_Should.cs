using EMS.Data;
using EMS.Services;
using EMS.Services.Contracts;
using EMS.Services.Factories.Contracts;
using EMS.Services.Tests;
using GmailAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace EMS.ServiceTests.ApplicationSerivceTests
{
    [TestClass]
    public class GetEmailId_Should
    {
        [TestMethod]
        public async Task GetEmailId()
        {
            TestUtils.GetContextWithApplications(nameof(GetEmailId));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetEmailId))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                var appId = TestUtils.Applications[0].Id;
                var emailId = await sut.GetEmailIdAsync(appId.ToString());

                Assert.AreEqual(TestUtils.Applications[0].EmailId.ToString(), emailId);
            }
        }
    }
}
