using BankingApp.DTOs;
using FluentValidation;

namespace BankingApp.Validators
{
    public class DepositRequestValidator : AbstractValidator<DepositRequestDto>
    {
        public DepositRequestValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);

            RuleFor(x => x.AccountId).GreaterThan(0);

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Deposit amount must be greater than zero.");
        }
    }
}
