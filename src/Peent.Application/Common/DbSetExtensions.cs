using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Peent.Application.Common
{
    public static class DbSetExtensions
    {
        public static ValueTask<TEntity> GetAsync<TEntity>(this DbSet<TEntity> dbSet, object keyValue, CancellationToken cancellationToken)
            where TEntity : class
        {
            return dbSet.FindAsync(new [] {keyValue}, cancellationToken);
        }
    }
}