using Peent.Application.Specifications;
using Peent.Domain.Entities;
using Peent.Domain.Entities.TransactionAggregate;

namespace Peent.Application.Transactions.Specifications
{
    public class TransactionDetails : BaseSpecification<Transaction>
    {
        public TransactionDetails()
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Entries);
            AddInclude($"{nameof(Transaction.Entries)}.{nameof(TransactionEntry.Account)}");
            AddInclude($"{nameof(Transaction.Entries)}.{nameof(TransactionEntry.Account)}.{nameof(Account.Currency)}");
            AddInclude(x => x.TransactionTags);
            AddInclude($"{nameof(Transaction.TransactionTags)}.{nameof(TransactionTag.Tag)}");
        }
    }
}
