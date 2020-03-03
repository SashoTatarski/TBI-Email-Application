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
    public class GetOperatorUsernameAsync_Should
    {
        [TestMethod]
        public async Task GetOperatorUsername()
        {
            TestUtils.GetContextWithApplications(nameof(GetOperatorUsername));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetOperatorUsername))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                var actualUserName = TestUtils.Applications[0].User.UserName;

                var emailId = TestUtils.Applications[0].EmailId;
                var expectedUserName = await sut.GetOperatorUsernameAsync(emailId.ToString());

                Assert.AreEqual(expectedUserName, actualUserName);
            }
        }

        [TestMethod]
        public async Task GetOperatorUsername_Null()
        {
            TestUtils.GetContextWithApplications(nameof(GetOperatorUsername_Null));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetOperatorUsername_Null))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                var expectedUserName = await sut.GetOperatorUsernameAsync(null);

                Assert.AreEqual(expectedUserName, null);
            }
        }
    }
}
