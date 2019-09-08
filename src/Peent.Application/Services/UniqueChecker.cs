using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Interfaces;

namespace Peent.Application.Services
{
    public class UniqueChecker : IUniqueChecker
    {
        private readonly IApplicationDbContext _db;

        public UniqueChecker(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> IsUniqueAsync<TEntity>(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken cancellationToken) where TEntity : class
        {
            return false == await _db.Set<TEntity>()
                .Where(expression)
                .AnyAsync(cancellationToken);
        }
    }
}
