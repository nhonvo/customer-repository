using Application.ViewModels;
using FluentValidation;

namespace WebAPI.Validations
{
    public class CustomerDTOValidation : AbstractValidator<CustomerDTO>
    {
        public CustomerDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Phone).NotEmpty().Length(10);
            RuleFor(x => x.Wallet).GreaterThanOrEqualTo(0);
        }
    }
}
