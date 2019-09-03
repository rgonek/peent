﻿using FluentValidation;

namespace Peent.Application.Currencies.Commands.EditCurrency
{
    public class EditCurrencyCommandValidator : AbstractValidator<EditCurrencyCommand>
    {
        public EditCurrencyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(3);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(12);
            RuleFor(x => x.DecimalPlaces)
                .NotNull()
                .LessThanOrEqualTo((ushort)18);
        }
    }
}