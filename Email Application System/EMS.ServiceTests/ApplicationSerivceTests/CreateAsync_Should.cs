namespace EMS.ServiceTests.ApplicationSerivceTests
{
    //[TestClass]
    //public class CreateAsync_Should
    //{
    ////    [TestMethod]
    ////    public async Task Create()
    ////    {
    ////        TestUtils.GetContextWithApplications(nameof(Create));

    ////        var gmailServiceMock = new Mock<IGmailAPIService>();
    ////        var appFactoryMock = new Mock<IApplicationFactory>();
    ////        var userSericeMock = new Mock<IUserService>();

    ////        var newApplication = new ApplicationDomain()
    ////        {

    ////        };

    ////        using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(Create))))
    ////        {
    ////            var sut = new ApplicationService(assertContext, appFactoryMock.Object, userSericeMock.Object);

    ////            userSericeMock
    ////                .Setup(u => u.GetUserIdAsync("mockUsername"))
    ////                .ReturnsAsync("mockId");

    ////            appFactoryMock
    ////                .Setup(f => f.Create("mockEmailId", "mockUserId", "11111111", "mockName", "+35991111"))
    ////                .Returns(new ApplicationDomain()
    ////                {
    ////                    Id = Guid.NewGuid(),
    ////                    EGN = "1111",
    ////                    EmailId = Guid.NewGuid(),
    ////                    UserId = Guid.NewGuid().ToString(),
    ////                    Name = "testname",
    ////                    PhoneNumber = "+1111",
    ////                    Status = ApplicationStatus.NotReviewed
    ////                });

    ////            await sut.CreateAsync("mockEmailId", "mockUsername", "1111", "mockName", "+1111");

    ////            Assert.IsTrue(assertContext.Applications.Any(app => app.PhoneNumber == "+1111"));

    ////        }
    ////    }
    //}
}
