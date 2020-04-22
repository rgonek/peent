using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Peent.Application.Infrastructure
{
    public class TransactionBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IApplicationDbContext _db;

        public TransactionBehavior(IApplicationDbContext db) => _db = db;

        public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                await _db.BeginTransactionAsync();
                var response = await next();
                await _db.CommitTransactionAsync();
                return response;
            }
            catch (Exception)
            {
                _db.RollbackTransaction();
                throw;
            }
        }
    }
}
