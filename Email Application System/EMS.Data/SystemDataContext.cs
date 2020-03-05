using EMS.Data.Configurations;
using EMS.Data.dbo_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Data
{
    public class SystemDataContext : IdentityDbContext<UserDomain>
    {
        public SystemDataContext(DbContextOptions<SystemDataContext> options) : base(options)
        { }

        public DbSet<UserDomain> Users { get; set; }
        public DbSet<ApplicationDomain> Applications { get; set; }
        public DbSet<AttachmentDomain> Attachments { get; set; }
        public DbSet<EmailDomain> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationConfiguration());
            builder.ApplyConfiguration(new AttachmentConfiguration());
            builder.ApplyConfiguration(new EmailConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());


            base.OnModelCreating(builder);
        }
    }
}
