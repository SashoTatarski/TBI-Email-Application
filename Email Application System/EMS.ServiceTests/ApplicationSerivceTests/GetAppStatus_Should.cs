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
    public class GetAppStatus_Should
    {
        [TestMethod]
        public async Task GetAppStatus()
        {
            TestUtils.GetContextWithApplications(nameof(GetAppStatus));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetAppStatus))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                var mailId = TestUtils.Applications[0].EmailId;
                var appStatus = await sut.GetStatusAsync(mailId.ToString());

                Assert.AreEqual(TestUtils.Applications[0].Status.ToString(), appStatus);
            }
        }
    }
}
