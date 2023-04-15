using Domain.Enum;

namespace Applications.ViewModels.UserViewModels
{
    public class RegisterRequestViewModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Role Role { get; set; }
    }
}
