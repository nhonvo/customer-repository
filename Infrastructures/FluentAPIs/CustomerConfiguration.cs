using System.IO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructures.FluentAPIs
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Orders)
                    .WithOne(x => x.Customer)
                    .HasForeignKey(x => x.CustomerId);
            // for concurrency conflicts
            builder.Property(x => x.Wallet)
                    .IsConcurrencyToken();
            // prop for global filter
            builder.HasQueryFilter(x => x.IsActive == true);

            builder.HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Name1",
                    Phone = "012345678",
                    Wallet = 500,
                    IsActive = true,
                },
                new Customer
                {
                    Id = 2,
                    Name = "name2",
                    Phone = "012345678",
                    Wallet = 500,
                    IsActive = true,
                },
                new Customer
                {
                    Id = 3,
                    Phone = "012345678",
                    Wallet = 500,
                    Name = "name3",
                    IsActive = false,
                }
            );

        }
    }
}