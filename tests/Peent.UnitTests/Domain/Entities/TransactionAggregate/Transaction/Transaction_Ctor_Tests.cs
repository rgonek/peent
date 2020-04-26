﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Peent.Domain.Entities;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.TransactionAggregate.Transaction;

namespace Peent.UnitTests.Domain.Entities.TransactionAggregate.Transaction
{
    public class Transaction_Ctor_Tests
    {
        private readonly IFixture _fixture = new Fixture();

        public Transaction_Ctor_Tests()
        {
            _fixture.Customizations.Insert(0, new FixedConstructorParameter<AccountType>(
                AccountType.Asset, nameof(Peent.Domain.Entities.Account.Type).FirstDown()));
            var entries = new List<Peent.Domain.Entities.TransactionAggregate.TransactionEntry>
            {
                _fixture.Create<Peent.Domain.Entities.TransactionAggregate.TransactionEntry>(),
                _fixture.Create<Peent.Domain.Entities.TransactionAggregate.TransactionEntry>()
            };
            _fixture.Customizations.Add(new FixedConstructorParameter<IEnumerable<Peent.Domain.Entities.TransactionAggregate.TransactionEntry>>(
                entries, nameof(Sut.Entries).FirstDown()));
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

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void when_category_id_is_not_positive__throws_argument_exception(int categoryId)
        {
            var parameterName = nameof(Sut.CategoryId).FirstDown();
            var customizer = new FixedConstructorParameter<int>(categoryId, parameterName);
            _fixture.Customizations.Add(customizer);

            Action act = () => Create<Sut>(_fixture);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_category_id_is_positive__does_not_throw()
        {
            var categoryId = _fixture.Create<int>();
            var customizer = new FixedConstructorParameter<int>(
                categoryId, nameof(Sut.CategoryId).FirstDown());
            _fixture.Customizations.Add(customizer);

            var account = Create<Sut>(_fixture);

            account.CategoryId.Should().Be(categoryId);
        }

        [Fact]
        public void when_from_account_is_null__throws_argument_exception()
        {
            var parameterName = "from" + nameof(Peent.Domain.Entities.Account).FirstDown();
            Peent.Domain.Entities.Account account = null;
            var customizer = new FixedConstructorParameter<Peent.Domain.Entities.Account>(
                account, parameterName);
            _fixture.Customizations.Insert(1, customizer);

            Action act = () => Create<Sut>(_fixture);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_to_account_is_null__throws_argument_exception()
        {
            var parameterName = "to" + nameof(Peent.Domain.Entities.Account).FirstDown();
            Peent.Domain.Entities.Account account = null;
            var customizer = new FixedConstructorParameter<Peent.Domain.Entities.Account>(
                account, parameterName);
            _fixture.Customizations.Insert(1, customizer);

            Action act = () => Create<Sut>(_fixture);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_tags_are_null__does_not_throw()
        {
            var parameterName = nameof(Sut.TransactionTags).FirstDown();
            var customizer = new FixedConstructorParameter<IEnumerable<Peent.Domain.Entities.TransactionAggregate.TransactionTag>>(
                null, parameterName);
            _fixture.Customizations.Add(customizer);
            _fixture.Customize<Sut>(c =>
                c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));

            var transaction = Create<Sut>(_fixture);

            transaction.TransactionTags.Should().BeEmpty();
        }

        [Fact]
        public void when_all_parameters_are_valid__correctly_set_properties()
        {
            var title = _fixture.Create<string>();
            var date = _fixture.Create<DateTime>();
            var description = _fixture.Create<string>();
            var categoryId = _fixture.Create<int>();
            var transactionTags = _fixture.Create<List<Peent.Domain.Entities.TransactionAggregate.TransactionTag>>();
            var amount = _fixture.Create<decimal>();
            var fromAccount = _fixture.Create<Peent.Domain.Entities.Account>();
            var toAccount = _fixture.Create<Peent.Domain.Entities.Account>();

            var transaction = new Sut(title, date, description, categoryId, amount, fromAccount, toAccount, transactionTags);

            transaction.Title.Should().Be(title);
            transaction.Date.Should().Be(date);
            transaction.Description.Should().Be(description);
            transaction.CategoryId.Should().Be(categoryId);
            // TODO: check entries
            //transaction.Entries.Should().BeEquivalentTo(entries);
            transaction.TransactionTags.Should().BeEquivalentTo(transactionTags);
        }
    }
}