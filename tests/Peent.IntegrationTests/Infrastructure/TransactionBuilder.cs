using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Peent.Application.Transactions.Commands.CreateTransaction;
using AutoFixture;
using Peent.Domain.Entities;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Infrastructure
{
    public class TransactionBuilder
    {
        public string _title;
        public string _description;
        public DateTime _date;
        public int _categoryId;
        public IList<int> _tagIds;
        public int _sourceAccountId;
        public int _destinationAccountId;
        public decimal _amount;

        public TransactionBuilder WithRandomData()
        {
            _title = F.Create<string>();
            _description = F.Create<string>();
            _date = F.Create<DateTime>();
            _amount = F.Create<decimal>();
            return this;
        }

        public TransactionBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public TransactionBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public TransactionBuilder AtDate(DateTime date)
        {
            _date = date;
            return this;
        }

        public TransactionBuilder From(Account account)
        {
            _sourceAccountId = account.Id;
            return this;
        }

        public TransactionBuilder To(Account account)
        {
            _destinationAccountId = account.Id;
            return this;
        }

        public TransactionBuilder Tagged(params Tag[] tags)
        {
            _tagIds = tags.Select(x => x.Id).ToList();
            return this;
        }

        public TransactionBuilder WithCategory(Category category)
        {
            _categoryId = category.Id;
            return this;
        }

        public CreateTransactionCommand AsCommand()
        {
            return new CreateTransactionCommand
            {
                Amount = _amount,
                CategoryId = _categoryId,
                Date = _date,
                Description = _description,
                SourceAccountId = _sourceAccountId,
                DestinationAccountId = _destinationAccountId,
                TagIds = _tagIds,
                Title = _title
            };
        }

        public async Task<Transaction> Build()
        {
            var transaction = new Transaction
            {
                Description = _description
            };

            await InsertAsync(transaction);

            return transaction;
        }

        public static implicit operator Transaction(TransactionBuilder builder)
        {
            return builder.Build().GetAwaiter().GetResult();
        }
    }
}
