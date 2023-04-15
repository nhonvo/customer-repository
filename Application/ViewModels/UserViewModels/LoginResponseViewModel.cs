namespace Applications.ViewModels.UserViewModels
{
    public class LoginResponseViewModel
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime? ExpireDay { get; set; }
    }
}
