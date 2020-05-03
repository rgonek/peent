using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using AutoFixture;
using Peent.Application;
using Peent.Application.Common.Validators.ExistsValidator;
using Peent.Application.Common.Validators.UniqueValidator;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static Peent.CommonTests.Infrastructure.TestFixture;

namespace Peent.IntegrationTests.Common.Validators
{
    public class ValidationFixture
    {
        public static ValueTask<List<ValidationFailure>> ValidateUniqueAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
            => ValidateAsync<TEntity, TestCommand>(x => x.Id, null,
                (db, userAccessor) =>
                    new UniqueInCurrentContextValidator<TestCommand, TEntity>(db, userAccessor, cmd => predicate));

        public static ValueTask<List<ValidationFailure>> ValidateExistsAsync<TEntity>(object commandValue)
            where TEntity : class
            => ValidateAsync<TEntity>(commandValue,
                (db, userAccessor) => new ExistsInCurrentContextValidator<TEntity>(db, userAccessor));

        public static ValueTask<List<ValidationFailure>> ValidateAsync<TEntity>(
            object commandValue,
            Func<IApplicationDbContext, IUserAccessor, IPropertyValidator> validatorFactory)
            where TEntity : class
            => ValidateAsync<TEntity, TestCommand>(x => x.Id, commandValue, validatorFactory);

        public static ValueTask<List<ValidationFailure>> ValidateAsync<TEntity, TCommand>(
            Expression<Func<TCommand, object>> commandPropertyPicker,
            object commandValue,
            Func<IApplicationDbContext, IUserAccessor, IPropertyValidator> validatorFactory)
            where TEntity : class
            => ExecuteDbContextAsync(async db =>
            {
                var validator = validatorFactory(db, UserAccessor);

                var command = F.Build<TCommand>()
                    .With(commandPropertyPicker, commandValue)
                    .Create();

                var validationContext = new ValidationContext(command);
                var rule = PropertyRule.Create(commandPropertyPicker);
                var propertyValidatorContext = new PropertyValidatorContext(validationContext, rule, "");
                return (await validator.ValidateAsync(propertyValidatorContext, default)).ToList();
            });

        private class TestCommand
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}