using EMS.Data;
using EMS.Data.Enums;
using EMS.Services;
using EMS.Services.Tests;
using GmailAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.ServiceTests.EmailServiceTests
{
    [TestClass]
    public class ChangeStatusAsync_Should
    {
        [TestMethod]
        public async Task ChangeStatus()
        {
            TestUtils.GetContextWithEmails(nameof(ChangeStatus));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(ChangeStatus))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var id = TestUtils.Emails[0].Id;

                await sut.ChangeStatusAsync(id.ToString(), EmailStatus.Open);

                var email = assertContext.Emails.FirstOrDefault(mail => mail.Id == id);

                Assert.IsTrue(email.Status == EmailStatus.Open);
            }
        }


        [TestMethod]
        public async Task ChangeStatusToNew()
        {
            TestUtils.GetContextWithEmails(nameof(ChangeStatusToNew));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(ChangeStatusToNew))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var id = TestUtils.Emails[0].Id;

                await sut.ChangeStatusAsync(id.ToString(), EmailStatus.New);

                var email = assertContext.Emails.FirstOrDefault(mail => mail.Id == id);

                var date = new DateTime(2019, 11, 22);
                email.ToNewStatus = date;

                Assert.IsTrue(email.ToNewStatus == date);
            }
        }

        [TestMethod]
        public async Task ChangeStatusToClosed()
        {
            TestUtils.GetContextWithEmails(nameof(ChangeStatusToClosed));

            var gmailServiceMock = new Mock<IGmailAPIService>();

            using (var assertContext = new SystemDataContext(TestUtils.GetOptions(nameof(ChangeStatusToClosed))))
            {
                var sut = new EmailService(assertContext, gmailServiceMock.Object);

                var id = TestUtils.Emails[0].Id;

                await sut.ChangeStatusAsync(id.ToString(), EmailStatus.Closed);

                var email = assertContext.Emails.FirstOrDefault(mail => mail.Id == id);

                var date = new DateTime(2019, 11, 22);
                email.ToTerminalStatus = date;

                Assert.IsTrue(email.ToTerminalStatus == date);
            }
        }
    }
}
