using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entiry;
using RealEstate.Domain.InterFace.Repository;
using RealEstate.Domain.InterFace.Specification;
using RealEstate.Reopsitory.Context;
using RealEstate.Reopsitory.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Reopsitory.Repossitory
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly EstateContext _context;

        public GenericRepository(EstateContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Property))
            {
                return (IReadOnlyList<TEntity>)await _context.Properties.Include(p=>p.Category).ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specification)
        {
            return await ApplaySpecification(specification).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id) => (await _context.Set<TEntity>().FindAsync(id))!;

        public async Task<TEntity> GetByIdWithSpecificationAsync(ISpecification<TEntity> specification)
        {
            return (await ApplaySpecification(specification).FirstOrDefaultAsync())!;
        }

        public async Task<int> GetPropertyCountWithSpecification(ISpecification<TEntity> specification)
        {
            return await ApplaySpecification(specification).CountAsync();
        }

        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);


        private IQueryable<TEntity> ApplaySpecification(ISpecification<TEntity> specification)
            => SpecificationEvaluator<TEntity, TKey>.BulidQuery(_context.Set<TEntity>(), specification);
    }
}
