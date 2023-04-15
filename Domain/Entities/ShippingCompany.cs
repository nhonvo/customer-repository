using System.Collections.Generic;

namespace Domain.Entities
{
    public class ShippingCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

}
