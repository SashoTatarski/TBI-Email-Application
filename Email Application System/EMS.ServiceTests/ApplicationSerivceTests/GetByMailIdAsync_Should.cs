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
    public class GetByMailIdAsync_Should
    {
        [TestMethod]
        public async Task GetByMailId()
        {
            TestUtils.GetContextWithApplications(nameof(GetByMailId));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetByMailId))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                var emailId = TestUtils.Applications[0].EmailId;
                var application = await sut.GetAppByMailIdAsync(emailId.ToString());

                Assert.AreEqual(TestUtils.Applications[0].UserId, application.UserId);
            }
        }
    }
}
