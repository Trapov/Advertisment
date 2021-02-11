using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repositories;
using Advertisement.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Advertisement.Infrastructure.DataAccess.Repositories
{
    public class EfRepository<TEntity, TId>
        : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
    {

        private readonly DatabaseContext _dbСontext;

        public EfRepository(DatabaseContext dbСontext)
        {
            _dbСontext = dbСontext;
        }

        public async Task<TEntity> FindById(TId id, CancellationToken cancellationToken)
        {
            return await _dbСontext.FindAsync<TEntity>(new object[] {id}, cancellationToken: cancellationToken);
        }

        public async Task Save(TEntity entity, CancellationToken cancellationToken)
        {
            var entry = _dbСontext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                await _dbСontext.AddAsync(entity, cancellationToken);
            }

            await _dbСontext.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken)
        {
            var data = _dbСontext.Set<TEntity>();

            return await data.Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> Count(CancellationToken cancellationToken)
        {
            var data = _dbСontext.Set<TEntity>();

            return await data.CountAsync(cancellationToken);
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {

            var data = _dbСontext.Set<TEntity>();

            return await data.Where(predicate).CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            var data = _dbСontext.Set<TEntity>();

            return await data.OrderBy(e => e.Id).Take(limit).Skip(offset).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetPaged(Expression<Func<TEntity, bool>> predicate, int offset,
            int limit, CancellationToken cancellationToken)
        {
            var data = _dbСontext.Set<TEntity>();

            return await data.Where(predicate).OrderBy(e => e.Id).Take(limit).Skip(offset)
                .ToListAsync(cancellationToken);
        }
    }
}