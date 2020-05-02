using System;
using FluentValidation;

namespace Peent.Application.Common.Validators
{
    public static class ValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, TElement> MustExistsInAuthenticationContext<T, TElement>(
            this IRuleBuilder<T, TElement> ruleBuilder, Type typeEntity, IApplicationDbContext db, IUserAccessor userAccessor)
        {
            return ruleBuilder.SetValidator(new ExistInAuthenticationContextValidator(typeEntity, db, userAccessor));
        }
    }
}