using System.ComponentModel.DataAnnotations;

namespace AdminDashBoard.Models
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage ="Name Is Requird")]
        [MaxLength(30),MinLength(4)]
        public string Name { get; set; }
    }
}
