using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(EMS.WebProject.Areas.Identity.IdentityHostingStartup))]
namespace EMS.WebProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}