using System;
using AuthenticationMicroservice.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationMicroservice.Persistence.Configs
{
    public class UserConfig: IEntityTypeConfiguration<Domain.Models.User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(m => m.Id);
            builder.Property<Guid>(m => m.Id).ValueGeneratedOnAdd();
            builder.Property(m => m.RegisterDateTime).IsRequired();
            builder.Property(m => m.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(m => m.PasswordHash).IsRequired();
            builder.Property(m => m.PasswordSalt).IsRequired();
            builder.Property(m => m.Username).IsRequired().IsUnicode().HasMaxLength(254);
            //builder.HasMany(m => m.Roles).WithOne().IsRequired();
            //builder.HasMany(m => m.Permissions).WithOne().IsRequired();
            //builder.HasOne(m => m.UserInformation).WithOne().IsRequired();


            //var navigationRole = builder.Metadata.FindNavigation(nameof(User.Roles));
            //navigationRole.SetPropertyAccessMode(PropertyAccessMode.Field);

            //var navigationPermission = builder.Metadata.FindNavigation(nameof(User.Permissions));
            //navigationPermission.SetPropertyAccessMode(PropertyAccessMode.Field);

            //var navigationUserInformation = builder.Metadata.FindNavigation(nameof(User.UserInformation));
            //navigationUserInformation.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
