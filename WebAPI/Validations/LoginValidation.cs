using Applications.ViewModels.UserViewModels;
using FluentValidation;

namespace APIs.Validations
{
    public class LoginValidation : AbstractValidator<LoginRequestViewModel>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty()
                                  .NotNull()
                                  .WithMessage("User Name cannot be Null or Empty!!!");
            RuleFor(x => x.PassWord).NotEmpty()
                                  .NotNull()
                                  .WithMessage("Password cannot be Null or Empty!!!");
        }
    }
}
