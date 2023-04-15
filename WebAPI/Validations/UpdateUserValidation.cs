using Applications.ViewModels.UserViewModels;
using FluentValidation;

namespace APIs.Validations
{
    public class UpdateUserValidation : AbstractValidator<UpdateRequestViewModel>
    {
        public UpdateUserValidation()
        {
            RuleFor(x => x.PassWord).NotEmpty()
                                    .NotNull()
                                    .WithMessage("Password cannot be Null or Empty!!!")
                                    .MinimumLength(5).WithMessage("Password lenght must greater than 5 characters");
            RuleFor(x => x.UserName).NotEmpty()
                                .NotNull()
                                .WithMessage("User Name cannot be Null or Empty!!!");
        }
    }
}
