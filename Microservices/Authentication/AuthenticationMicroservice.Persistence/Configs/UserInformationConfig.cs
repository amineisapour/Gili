using System;
using AuthenticationMicroservice.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationMicroservice.Persistence.Configs
{
    public class UserInformationConfig : IEntityTypeConfiguration<Domain.Models.UserInformation>
    {
        public void Configure(EntityTypeBuilder<UserInformation> builder)
        {
            builder.ToTable("UserInformation");
            builder.HasKey(m => m.Id);
            builder.Property<Guid>(m => m.Id).ValueGeneratedOnAdd();
            builder.Property(m => m.RegisterDateTime).IsRequired();
            builder.Property(m => m.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(m => m.FirstName).IsRequired();
            builder.Property(m => m.LastName).IsRequired();
            builder.Property(m => m.Birthdate).IsRequired();
            //builder.Property(m => m.UserId).IsRequired();
            builder.HasOne(m => m.User).WithOne().IsRequired();


            //var navigationUser = builder.Metadata.FindNavigation(nameof(UserInformation.User));
            //navigationUser.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
