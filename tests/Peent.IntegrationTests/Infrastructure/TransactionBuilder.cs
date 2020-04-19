using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Peent.Application.Transactions.Commands.CreateTransaction;
using AutoFixture;
using Peent.Domain.Entities;
using Peent.Domain.Entities.TransactionAggregate;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Infrastructure
{
    public class TransactionBuilder
    {
        private string _title;
        private string _description;
        private DateTime _date;
        private int _categoryId;
        private IList<int> _tagIds;
        private Account _sourceAccount;
        private Account _destinationAccount;
        private decimal _amount;

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
            _sourceAccount = account;
            return this;
        }

        public TransactionBuilder To(Account account)
        {
            _destinationAccount = account;
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
                SourceAccountId = _sourceAccount.Id,
                DestinationAccountId = _destinationAccount.Id,
                TagIds = _tagIds,
                Title = _title
            };
        }

        public async Task<Transaction> Build()
        {
            var sourceEntry = new TransactionEntry(_sourceAccount, -_amount, _sourceAccount.CurrencyId);
            var destinationEntry = new TransactionEntry(_destinationAccount, _amount, _sourceAccount.CurrencyId);
            var transaction = new Transaction(_title, _date, _description, _categoryId,
                new[] {sourceEntry, destinationEntry});
            foreach (var tagId in _tagIds)
            {
                transaction.AddTag(tagId);
            }

            await InsertAsync(transaction);

            return transaction;
        }

        public static implicit operator Transaction(TransactionBuilder builder)
        {
            return builder.Build().GetAwaiter().GetResult();
        }
    }
}
