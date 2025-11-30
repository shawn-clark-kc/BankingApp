using BankingApp.DTOs;

namespace BankingApp.Services
{
    public interface IAccountService
    {
        DepositResponseDto Deposit(DepositRequestDto request);
        WithdrawResponseDto Withdraw(WithdrawRequestDto request);
        CloseAccountResponseDto CloseAccount(CloseAccountRequestDto request);
        CreateAccountResponseDto CreateAccount(CreateAccountRequestDto request);
    }
}
