namespace Application.ViewModels
{
    public class CustomerUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int? Wallet { get; set; }
        public bool? IsActive { get; set; }
    }
}