using BankingApp.DTOs;
using FluentValidation;

namespace BankingApp.Validators
{
    public class WithdrawRequestValidator : AbstractValidator<WithdrawRequestDto>
    {
        public WithdrawRequestValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);

            RuleFor(x => x.AccountId).GreaterThan(0);

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Withdrawal amount must be greater than zero.");
        }
    }
}
