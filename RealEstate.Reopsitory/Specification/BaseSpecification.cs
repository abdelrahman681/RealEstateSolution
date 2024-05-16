using RealEstate.Domain.InterFace.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Reopsitory.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {

        public Expression<Func<T, bool>> WhereExpression { get; }

        public BaseSpecification(Expression<Func<T, bool>> whereExpression)
        {
            WhereExpression = whereExpression;
        }

        public List<Expression<Func<T, object>>> IncludeExpression { get; } = new();

        public Expression<Func<T, object>> OrderByAsc { get; protected set; }

        public Expression<Func<T, object>> OrderByDesc { get; protected set; }

        public int Skip { get; protected set; }

        public int Take { get; protected set; }

        public bool ISpaginated { get; protected set; }

        protected void ApplayPagination(int pageSize, int pageIndex)
        {
            ISpaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;

            //pageSize is number of element in page 
        }
    }
}
