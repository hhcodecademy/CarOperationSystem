namespace CarOperationSystem.UI.Areas.Admin.Models
{
    public class UserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public IList<string>? Roles { get; set; }
    }
}
