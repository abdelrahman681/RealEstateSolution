using Microsoft.EntityFrameworkCore.Query;
using RealEstate.Domain.Entiry;
using RealEstate.Domain.InterFace.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Reopsitory.Specification
{
    public class PropertySpecification : BaseSpecification<Property>
    {

        //Get Property With Filtration
        public PropertySpecification(PropertySpecificationParameters parameters) : base(prop =>
        (!parameters.CategoryId.HasValue || prop.CategoryId == parameters.CategoryId.Value) &&
        (string.IsNullOrWhiteSpace(parameters.Search) || prop.Name!.ToLower().Contains(parameters.Search)))
        {
            IncludeExpression.Add(prop => prop.Category);


            if (parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {

                    case OrderSpecification.NameAsc:
                        OrderByAsc = x => x.Name!;
                        break;
                    case OrderSpecification.NameDesc:
                        OrderByDesc = x => x.Name!;
                        break;
                    case OrderSpecification.PriceAsc:
                        OrderByAsc = x => x.Price;
                        break;
                    case OrderSpecification.PriceDesc:
                        OrderByAsc = x => x.Price;
                        break;
                    default:
                        OrderByAsc = x => x.Name!;
                        break;
                }
            }

            ApplayPagination(parameters.PageSize, parameters.PageIndex);

        }

        //Get Property With Id
        public PropertySpecification(int id) : base(p => p.Id == id)
        {
            IncludeExpression.Add(prop => prop.Category);
        }
    }
}
