using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructures.FluentAPIs
{
    public class ShippingCompanyConfiguration : IEntityTypeConfiguration<ShippingCompany>
    {
        public void Configure(EntityTypeBuilder<ShippingCompany> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Orders)
                .WithOne(x => x.ShippingCompany)
                .HasForeignKey(x => x.ShippingCompanyId);

            builder.HasData(
                new ShippingCompany
                {
                    Id = 1,
                    Name = "Company 1"
                },
                new ShippingCompany
                {
                    Id = 2,
                    Name = "Company 2"
                },
                new ShippingCompany
                {
                    Id = 3,
                    Name = "Company 3"
                },
                new ShippingCompany
                {
                    Id = 4,
                    Name = "Company 4"
                }
            );
        }
    }
}