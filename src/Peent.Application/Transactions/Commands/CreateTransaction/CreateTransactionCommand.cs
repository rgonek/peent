using System;
using System.Collections.Generic;
using MediatR;

namespace Peent.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public IList<int> TagIds { get; set; }

        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }

        public decimal Amount { get; set; }
    }
}
