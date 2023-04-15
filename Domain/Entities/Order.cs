using System.Collections.Generic;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int Detail { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ShippingCompanyId { get; set; }
        public ShippingCompany ShippingCompany { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
