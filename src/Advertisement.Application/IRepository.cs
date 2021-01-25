using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Domain.Shared;

namespace Advertisement.Application
{
    public interface IRepository<TEntity, in TId>
        where TEntity: Entity<TId>
    {
        Task<TEntity> FindById(TId id, CancellationToken cancellationToken);
        Task Save(TEntity entity, CancellationToken cancellationToken);


        Task<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<int> Count(CancellationToken cancellationToken);
        Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        
        Task<IEnumerable<TEntity>> GetPaged(int offset, int limit, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetPaged(Expression<Func<TEntity, bool>> predicate, int offset, int limit,
            CancellationToken cancellationToken);
    }

    public static class Paged
    {
        public abstract class Request
        {
            public int Offset { get; set; } = 0;
            public int Limit { get; set; } = 10;
        }

        public abstract class Response<T>
        {
            public int Total { get; set; }
            public int Limit { get; set; }
            public int Offset { get; set; }
            
            public IEnumerable<T> Items { get; set; }
        } 
    }
}