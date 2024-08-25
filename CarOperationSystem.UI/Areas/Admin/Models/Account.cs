using System.ComponentModel.DataAnnotations;

namespace CarOperationSystem.UI.Areas.Admin.Models
{
    public class Account
    {
 
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
    }
}
