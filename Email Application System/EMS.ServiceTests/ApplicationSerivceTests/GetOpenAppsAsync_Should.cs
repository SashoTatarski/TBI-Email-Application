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
    public class GetOpenAppsAsync_Should
    {
        [TestMethod]
        public async Task GetOpenApps()
        {
            TestUtils.GetContextWithApplications(nameof(GetOpenApps));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(GetOpenApps))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                var openApps = await sut.GetOpenAppsAsync();

                var expectedOpenAppsCount = TestUtils.Applications
                    .Count(app => app.Status == ApplicationStatus.NotReviewed);

                var actualOpenAppsCount = openApps.Count();

                Assert.AreEqual(expectedOpenAppsCount, actualOpenAppsCount);
            }
        }

        [TestMethod]
        public async Task MapsCorrectly_Id()
        {
            TestUtils.GetContextWithApplications(nameof(MapsCorrectly_Id));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_Id))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                var openApps = await sut.GetOpenAppsAsync();

                Assert.IsTrue(openApps.Any(app => app.Id == TestUtils.Applications[0].Id));
            }
        }

        [TestMethod]
        public async Task MapsCorrectly_EGN()
        {
            TestUtils.GetContextWithApplications(nameof(MapsCorrectly_EGN));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_EGN))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                var openApps = await sut.GetOpenAppsAsync();


                Assert.IsTrue(openApps.Any(app => app.EGN == TestUtils.Applications[0].EGN));
            }
        }

        [TestMethod]
        public async Task MapsCorrectly_Name()
        {
            TestUtils.GetContextWithApplications(nameof(MapsCorrectly_Name));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_Name))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                var openApps = await sut.GetOpenAppsAsync();

                Assert.IsTrue(openApps.Any(app => app.Name == TestUtils.Applications[0].Name));
            }
        }

        [TestMethod]
        public async Task MapsCorrectly_Status()
        {
            TestUtils.GetContextWithApplications(nameof(MapsCorrectly_Status));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_Status))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                var openApps = await sut.GetOpenAppsAsync();

                Assert.IsTrue(openApps.Any(app => app.Status == TestUtils.Applications[0].Status));
            }
        }

        [TestMethod]
        public async Task MapsCorrectly_PhoneNumber()
        {
            TestUtils.GetContextWithApplications(nameof(MapsCorrectly_PhoneNumber));

            var gmailServiceMock = new Mock<IGmailAPIService>();
            var appFactoryMock = new Mock<IApplicationFactory>();
            var userSericeMock = new Mock<IUserService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(MapsCorrectly_PhoneNumber))))
            {
                var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

                var openApps = await sut.GetOpenAppsAsync();

                Assert.IsTrue(openApps.Any(app => app.PhoneNumber == TestUtils.Applications[0].PhoneNumber));
            }
        }
    }
}
