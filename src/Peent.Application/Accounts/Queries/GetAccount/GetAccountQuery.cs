using MediatR;
using Peent.Application.Accounts.Models;

namespace Peent.Application.Accounts.Queries.GetAccount
{
    public class GetAccountQuery : IRequest<AccountModel>
    {
        public int Id { get; set; }
    }
}
