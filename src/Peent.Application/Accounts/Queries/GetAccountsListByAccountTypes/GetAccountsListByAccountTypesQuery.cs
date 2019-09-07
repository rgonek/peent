using System.Collections.Generic;
using MediatR;
using Peent.Application.Accounts.Models;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Queries.GetAccountsListByAccountTypes
{
    public class GetAccountsListByAccountTypesQuery : IRequest<IList<AccountModel>>
    {
        public AccountType[] Types { get; set; }
    }
}
