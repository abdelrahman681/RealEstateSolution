using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Entiry
{
    public class Property :BaseEntity<int>
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int NofBed { get; set; }
        public int NofBath { get; set; }
        public decimal Price { get; set; }
        public string? PictureUrl { get; set; }
        public string? Mode { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int? Size { get; set; }
    }
}
