using EMS.Data;
using EMS.Data.Enums;
using EMS.Services;
using EMS.Services.Contracts;
using EMS.Services.Factories.Contracts;
using EMS.Services.Tests;
using GmailAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.ServiceTests.ApplicationSerivceTests
{
    [TestClass]
    public class ChangeStatusAsync_Should
    {
        [TestMethod]
        public async Task ChangeStatus()
        {
            TestUtils.GetContextWithApplications(nameof(ChangeStatus));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(ChangeStatus))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                var applicationId = TestUtils.Applications[0].Id;
                await sut.ChangeStatusAsync(applicationId.ToString(), ApplicationStatus.Approved, "userName");

                var application = assertContext.Applications.FirstOrDefault(mail => mail.Id == applicationId);

                Assert.AreEqual(application.Status, ApplicationStatus.Approved);
            }
        }
    }
}
