using System.Collections.Generic;
using MediatR;
using Peent.Application.Accounts.Models;

namespace Peent.Application.Accounts.Queries.GetAccountsList
{
    public class GetAccountsListQuery : IRequest<IList<AccountModel>>
    {
    }
}
