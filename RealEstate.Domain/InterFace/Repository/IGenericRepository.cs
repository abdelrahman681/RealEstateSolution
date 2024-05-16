using RealEstate.Domain.Entiry;
using RealEstate.Domain.InterFace.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.InterFace.Repository
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specification);
        Task<int> GetPropertyCountWithSpecification(ISpecification<TEntity> specification);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> GetByIdWithSpecificationAsync(ISpecification<TEntity> specification);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);


    }
}
