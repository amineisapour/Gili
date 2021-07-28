using System;
using AuthenticationMicroservice.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationMicroservice.Persistence.Configs
{
    public class PermissionConfig : IEntityTypeConfiguration<Domain.Models.Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");
            builder.HasKey(m => m.Id);
            builder.Property<Guid>(m => m.Id).ValueGeneratedOnAdd();
            builder.Property(m => m.RegisterDateTime).IsRequired();
            builder.Property(m => m.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(m => m.Name).IsRequired().IsUnicode().HasMaxLength(254);
        }
    }
}
