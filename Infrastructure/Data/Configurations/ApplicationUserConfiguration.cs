using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(u => u.LastName)
               .HasMaxLength(100)
               .IsRequired();

        builder.HasIndex(u => u.Email)
               .IsUnique();
    }
}