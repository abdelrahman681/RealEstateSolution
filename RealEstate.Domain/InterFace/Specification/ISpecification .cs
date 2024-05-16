using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.InterFace.Specification
{
    public interface ISpecification<T>
    {
        public Expression<Func<T, bool>> WhereExpression { get; }

        public List<Expression<Func<T, object>>> IncludeExpression { get; }

        public Expression<Func<T, object>> OrderByAsc { get; }

        public Expression<Func<T, object>> OrderByDesc { get; }

        public int Skip { get; }
        public int Take { get; }
        public bool ISpaginated { get; }
    }
}
