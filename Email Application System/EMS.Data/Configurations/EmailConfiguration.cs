using EMS.Data.dbo_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS.Data.Configurations
{
    public class EmailConfiguration : IEntityTypeConfiguration<EmailDomain>
    {
        public void Configure(EntityTypeBuilder<EmailDomain> builder)
        {
            builder
                .HasKey(email => email.Id);

            builder
                .HasOne(email => email.Application)
                .WithOne(app => app.Email);

            builder
                .HasMany(email => email.Attachments)
                .WithOne(att => att.Email);

            builder.ToTable("Emails");
        }
    }
}
