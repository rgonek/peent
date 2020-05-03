using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Accounts.Models;
using Peent.Application.Common;

namespace Peent.Application.Accounts.Queries.GetAccount
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountModel>
    {
        private readonly IApplicationDbContext _db;

        public GetAccountQueryHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<AccountModel> Handle(GetAccountQuery query, CancellationToken token)
        {
            var account = await _db.Accounts.GetAsync(query.Id, token);
            await _db.Entry(account).Reference(x => x.Currency).LoadAsync(token);

            return new AccountModel(account);
        }
    }
}