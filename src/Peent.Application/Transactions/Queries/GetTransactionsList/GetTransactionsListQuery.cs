using System;
using System.Collections.Generic;
using MediatR;
using Peent.Application.Infrastructure;
using Peent.Application.Transactions.Models;

namespace Peent.Application.Transactions.Queries.GetTransactionsList
{
    public class GetTransactionsListQuery : IRequest<IList<TransactionModel>>
    {
        public PageInfo PageInfo { get; set; }
        public IList<int> Accounts { get; set; }
        public IList<int> Categories { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
