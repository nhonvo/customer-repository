using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User()
                {
                    Id = 1,
                    UserName = "Hoa",
                    PasswordHash = "abcdefghijklmnopqrstuvwxyz",
                    DateOfBirth = new DateTime(2002, 10, 31),
                    Role = Role.Admin
                },
                new User()
                {
                    Id = 2,
                    UserName = "Mai",
                    PasswordHash = "abcdefghijklmnopqrstuvwxyz",
                    DateOfBirth = new DateTime(2002, 10, 31),
                    Role = Role.User
                },
                  new User()
                  {
                      Id = 4,
                      UserName = "Lan",
                      PasswordHash = "abcdefghijklmnopqrstuvwxyz",
                      DateOfBirth = new DateTime(2002, 10, 31),
                      Role = Role.User
                  }
            );
        }
    }
}