using BankingSolutionWebApi.Application.Transactions.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Application.Transactions.Validation
{
    public class FundsRequestValidator : AbstractValidator<FundsRequest>
    {
        public FundsRequestValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("Card number is required.")
                .Length(16).WithMessage("Card number must be 16 digits.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}
