using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.TransactionAggregate.Transaction;
using AccountEntity = Peent.Domain.Entities.Account;
using TagEntity = Peent.Domain.Entities.Tag;
using CategoryEntity = Peent.Domain.Entities.Category;

namespace Peent.UnitTests.Domain.Entities.TransactionAggregate.Transaction
{
    public class Transaction_Ctor_Tests
    {
        private readonly IFixture _fixture = new Fixture();

        public Transaction_Ctor_Tests()
        {
            _fixture.Customizations.Insert(0, new FixedConstructorParameter<AccountType>(
                AccountType.Asset, nameof(AccountEntity.Type).FirstDown()));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_title_is_null_or_white_space__throws_argument_exception(string title)
        {
            var parameterName = nameof(Sut.Title).FirstDown();
            var customizer = new FixedConstructorParameter<string>(
                title, parameterName);
            _fixture.Customizations.Add(customizer);

            Action act = () => Create<Sut>(_fixture);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_title_is_not_null_or_white_space__does_not_throw()
        {
            var title = _fixture.Create<string>();
            var customizer = new FixedConstructorParameter<string>(
                title, nameof(Sut.Title).FirstDown());
            _fixture.Customizations.Add(customizer);

            var transaction = Create<Sut>(_fixture);

            transaction.Title.Should().Be(title);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_description_is_null_or_white_space__does_not_throw(string description)
        {
            var parameterName = nameof(Sut.Description).FirstDown();
            var customizer = new FixedConstructorParameter<string>(
                description, parameterName);
            _fixture.Customizations.Add(customizer);
            _fixture.Customize<Sut>(c =>
                c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));

            var transaction = Create<Sut>(_fixture);

            transaction.Description.Should().Be(description);
        }

        [Fact]
        public void when_category_is_null__throws_argument_exception()
        {
            var parameterName = nameof(Sut.Category).FirstDown();
            var customizer = new FixedConstructorParameter<CategoryEntity>(null, parameterName);
            _fixture.Customizations.Add(customizer);

            Action act = () => Create<Sut>(_fixture);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_category_is_not_null__does_not_throw()
        {
            var category = _fixture.Create<CategoryEntity>();
            var customizer = new FixedConstructorParameter<CategoryEntity>(
                category, nameof(Sut.Category).FirstDown());
            _fixture.Customizations.Add(customizer);

            var account = Create<Sut>(_fixture);

            account.Category.Should().Be(category);
        }

        [Fact]
        public void when_from_account_is_null__throws_argument_exception()
        {
            var parameterName = "from" + nameof(Peent.Domain.Entities.Account);
            AccountEntity account = null;
            var customizer = new FixedConstructorParameter<AccountEntity>(
                account, parameterName);
            _fixture.Customizations.Insert(1, customizer);

            Action act = () => Create<Sut>(_fixture);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_to_account_is_null__throws_argument_exception()
        {
            var parameterName = "to" + nameof(Peent.Domain.Entities.Account);
            AccountEntity account = null;
            var customizer = new FixedConstructorParameter<AccountEntity>(
                account, parameterName);
            _fixture.Customizations.Insert(1, customizer);

            Action act = () => Create<Sut>(_fixture);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_tags_are_null__does_not_throw()
        {
            var transaction = new Sut(
                _fixture.Create<string>(),
                _fixture.Create<DateTime>(),
                _fixture.Create<CategoryEntity>(),
                _fixture.Create<decimal>(),
                _fixture.Create<AccountEntity>(),
                _fixture.Create<AccountEntity>(), 
                tags: null);

            transaction.TransactionTags.Should().BeEmpty();
        }

        [Fact]
        public void when_all_parameters_are_valid__correctly_set_properties()
        {
            var title = _fixture.Create<string>();
            var date = _fixture.Create<DateTime>();
            var description = _fixture.Create<string>();
            var category = _fixture.Create<CategoryEntity>();
            var tags = _fixture.Create<List<TagEntity>>();
            var amount = _fixture.Create<decimal>();
            var fromAccount = _fixture.Create<AccountEntity>();
            var toAccount = _fixture.Create<AccountEntity>();

            var transaction = new Sut(title, date, description, category, amount, fromAccount, toAccount, tags);

            transaction.Title.Should().Be(title);
            transaction.Date.Should().Be(date);
            transaction.Description.Should().Be(description);
            transaction.Category.Should().Be(category);
            transaction.TransactionTags.Select(x => x.Tag).Should().BeEquivalentTo(tags);
            transaction.Entries.Should().HaveCount(2);
            transaction.Entries.First().Account.Should().Be(fromAccount);
            transaction.Entries.First().Money.Should().Be(new Money(amount, fromAccount.Currency));
            transaction.Entries.Last().Account.Should().Be(toAccount);
            transaction.Entries.Last().Money.Should().Be(new Money(-amount, toAccount.Currency));
        }
    }
}