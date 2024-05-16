using RealEstate.Domain.Entiry;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminDashBoard.Models
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Location is Required")]
        public string? Location { get; set; }
        [Required(ErrorMessage = "NofBed is Required")]
        [Range(1, 12)]
        public int NofBed { get; set; }
        [Required(ErrorMessage = "NofBath is Required")]
        [Range(1,3)]
        public int NofBath { get; set; }
        [Required(ErrorMessage = "Price is Required")]
        public decimal Price { get; set; }

        public string? PictureUrl { get; set; } = null;
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string? Mode { get; set; }
        public Category? Category { get; set; }
        [Required(ErrorMessage = "CategoryId is Required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Size is Required")]
        public int? Size { get; set; }
    }
}
