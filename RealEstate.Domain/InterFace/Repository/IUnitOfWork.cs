using RealEstate.Domain.Entiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.InterFace.Repository
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        Task<int> CompleteAsync();
    }
}
