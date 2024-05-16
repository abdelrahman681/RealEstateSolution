using RealEstate.Domain.Entiry;
using RealEstate.Domain.InterFace.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Reopsitory.Specification
{
    public class PropertyCountWithSpecification : BaseSpecification<Property>
    {
        public PropertyCountWithSpecification(PropertySpecificationParameters parameters)
            : base(prop => (!parameters.CategoryId.HasValue || prop.CategoryId == parameters.CategoryId.Value) &&
            (string.IsNullOrWhiteSpace(parameters.Search) || prop.Name!.ToLower().Contains(parameters.Search)))
        {
        }
    }
}
