using BankingApp.DTOs;
using FluentValidation;

namespace BankingApp.Validators
{
    public class CloseAccountRequestValidator : AbstractValidator<CloseAccountRequestDto>
    {
        public CloseAccountRequestValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);

            RuleFor(x => x.AccountId).GreaterThan(0);
        }
    }
}
