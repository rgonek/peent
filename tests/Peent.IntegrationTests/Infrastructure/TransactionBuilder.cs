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
        private Category _category;
        private IList<Tag> _tags;
        private Account _fromAccount;
        private Account _toAccount;
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
            _fromAccount = account;
            return this;
        }

        public TransactionBuilder To(Account account)
        {
            _toAccount = account;
            return this;
        }

        public TransactionBuilder Tagged(params Tag[] tags)
        {
            _tags = tags.ToList();
            return this;
        }

        public TransactionBuilder WithCategory(Category category)
        {
            _category = category;
            return this;
        }

        public CreateTransactionCommand AsCommand()
        {
            return new CreateTransactionCommand
            {
                Amount = _amount,
                CategoryId = _category.Id,
                Date = _date,
                Description = _description,
                FromAccountId = _fromAccount.Id,
                ToAccountId = _toAccount.Id,
                TagIds = _tags?.Select(x => x.Id).ToList(),
                Title = _title
            };
        }

        public async Task<Transaction> Build()
        {
            var transaction = new Transaction(_title, _date, _description, _category,
                _amount, _fromAccount, _toAccount, _tags);

            await InsertAsync(transaction);

            return transaction;
        }

        public static implicit operator Transaction(TransactionBuilder builder)
        {
            return builder.Build().GetAwaiter().GetResult();
        }
    }
}
