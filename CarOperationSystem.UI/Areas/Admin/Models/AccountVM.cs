using System.ComponentModel.DataAnnotations;

namespace CarOperationSystem.UI.Areas.Admin.Models
{
    public class AccountVM
    {
 
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        public bool Persistent { get; set; }
    }
}
