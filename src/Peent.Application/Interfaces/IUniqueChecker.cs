using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Peent.Application.Interfaces
{
    public interface IUniqueChecker
    {
        Task<bool> IsUniqueAsync<TEntity>(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken cancellationToken) where TEntity : class;
    }
}
