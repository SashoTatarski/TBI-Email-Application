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

        public DbSet<UserDomain> Userss { get; set; }
        public DbSet<ApplicationDomain> Applicationss { get; set; }
        public DbSet<AttachmentDomain> Attachmentss { get; set; }
        public DbSet<EmailDomain> Emailss { get; set; }

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
