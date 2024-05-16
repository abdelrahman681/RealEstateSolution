using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.InterFace.Specification
{
    public class PropertySpecificationParameters
    {
        private const int MAXPAGESIZE = 30;
        public int? CategoryId { get; set; }
        public OrderSpecification? Sort { get; set; }

        public int PageIndex { get; set; } = 1;

        private int pagesize = 8;

        public int PageSize
        {
            get => pagesize;
            set { pagesize = value > MAXPAGESIZE ? MAXPAGESIZE : value; }
        }

        private string? search;

        public string? Search
        {
            get => search;
            set => search = value?.Trim().ToLower();
        }

    }
}
