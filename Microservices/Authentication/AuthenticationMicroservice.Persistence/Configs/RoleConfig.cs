using System;
using AuthenticationMicroservice.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationMicroservice.Persistence.Configs
{
    public class RoleConfig : IEntityTypeConfiguration<Domain.Models.Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(m => m.Id);
            builder.Property<Guid>(m => m.Id).ValueGeneratedOnAdd();
            builder.Property(m => m.RegisterDateTime).IsRequired();
            builder.Property(m => m.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(m => m.Name).IsRequired().IsUnicode().HasMaxLength(254);
            //builder.HasMany(m => m.Permissions).WithOne().IsRequired();

            //var navigationPermission = builder.Metadata.FindNavigation(nameof(Role.Permissions));
            //navigationPermission.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
