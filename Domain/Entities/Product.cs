using System.Collections.Generic;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}