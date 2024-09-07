using System.ComponentModel.DataAnnotations;

namespace CarOperationSystem.UI.Areas.Admin.Models
{
    public class RoleVM
    {
        public string? Id { get; set; }

        [Required]
        [Display(Name = "Rol adi")]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
