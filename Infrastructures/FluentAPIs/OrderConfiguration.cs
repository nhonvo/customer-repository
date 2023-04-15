using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructures.FluentAPIs
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);


            builder.HasData(
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    ShippingCompanyId = 1
                },
                new Order
                {
                    Id = 2,
                    CustomerId = 1,
                    ShippingCompanyId = 4
                },
                new Order
                {
                    Id = 3,
                    CustomerId = 3,
                    ShippingCompanyId = 1
                },
                new Order
                {
                    Id = 4,
                    CustomerId = 1,
                    ShippingCompanyId = 2
                },
                new Order
                {
                    Id = 5,
                    CustomerId = 1,
                    ShippingCompanyId = 3
                }
            );
        }
    }
}