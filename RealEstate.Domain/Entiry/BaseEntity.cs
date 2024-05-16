using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Entiry
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
