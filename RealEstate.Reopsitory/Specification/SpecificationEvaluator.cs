using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entiry;
using RealEstate.Domain.InterFace.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Reopsitory.Specification
{
    public class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> BulidQuery(IQueryable<TEntity> inputquery, ISpecification<TEntity> specification)
        {
            var query = inputquery;

            if (specification.WhereExpression is not null)

                query = query.Where(specification.WhereExpression);

            if (specification.IncludeExpression.Any())
            {
                foreach (var item in specification.IncludeExpression)
                    query = query.Include(item);
            }

            if (specification.OrderByAsc is not null)
                query = query.OrderBy(specification.OrderByAsc);

            if (specification.OrderByDesc is not null)
                query = query.OrderByDescending(specification.OrderByDesc);

            if (specification.ISpaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);
            return query;
        }

    }
}
