using RealEstate.Domain.Entiry;
using RealEstate.Domain.InterFace.Repository;
using RealEstate.Reopsitory.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Reopsitory.Repossitory
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EstateContext _context;
        private readonly Hashtable _Repo;

        public UnitOfWork(EstateContext context)
        {
            _context = context;
            _Repo = new Hashtable();
        }

        //save change happened in database
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        //dispose the connection after savechanges
        public ValueTask DisposeAsync() => _context.DisposeAsync();
        //
        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_Repo.ContainsKey(typeName))
                return (_Repo[typeName] as IGenericRepository<TEntity, TKey>)!;

            var repo = new GenericRepository<TEntity, TKey>(_context);
            _Repo.Add(typeName, repo);
            return repo;
        }
    }
}
