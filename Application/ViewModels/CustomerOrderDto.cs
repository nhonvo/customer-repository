using Domain.Entities;

namespace Application.ViewModels
{
    public class CustomerOrderDTO
    {
        public Customer Customer { get; set; }
        public Order Order { get; set; }
    }
}