using BankingApp.DTOs;
using FluentValidation;

namespace BankingApp.Validators
{
    public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequestDto>
    {
        public CreateAccountRequestValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);

            RuleFor(x => x.InitialDeposit).GreaterThanOrEqualTo(100).WithMessage("Initial deposit must be at least 100.");

            RuleFor(x => x.AccountTypeId).Must(id => id == 1 || id == 2).WithMessage("AccountTypeId must be 1 (Checking) or 2 (Savings).");
        }
    }
}
