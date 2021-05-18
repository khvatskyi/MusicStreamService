using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStreamServiceApp.DAL.Entities;

namespace MusicStreamServiceApp.DAL.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
               new User[] {
                    new User { UserName = "Carendoh", NormalizedUserName = "CARENDOH", FirstName = "Oleksandr", LastName = "Slobodian", Email = "test@gmail.com", NormalizedEmail = "TEST@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEEYOfbhhZP2uqLre7aNmNHV2NkGpy2nUQwx6Y8khu2hOAj2VZ1Q6hNyu34Yq0Dcl4g==" }
               });
        }
    }
}
