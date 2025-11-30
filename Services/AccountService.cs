using BankingApp.Domain.Entities;
using BankingApp.Domain.Enums;
using BankingApp.DTOs;
using BankingApp.Repositories;

namespace BankingApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IAccountRepository _accountRepo;

        public AccountService(ICustomerRepository customerRepo, IAccountRepository accountRepo)
        {
            _customerRepo = customerRepo;
            _accountRepo = accountRepo;
        }

        #region Deposit

        public DepositResponseDto Deposit(DepositRequestDto request)
        {
            var account = _accountRepo.GetById(request.AccountId);
            var customer = _customerRepo.GetById(request.CustomerId);

            if (customer is null)
                return FailDeposit("Customer does not exist.", request);

            if (account is null)
                return FailDeposit("Account does not exist.", request);

            if (account.CustomerId != request.CustomerId)
                return FailDeposit("Account does not belong to the specified customer.", request);

            if (account.Status != AccountStatus.Open)
                return FailDeposit("Account is not open.", request);

            if (request.Amount <= 0)
                return FailDeposit("Deposit amount must be greater than zero.", request);

            account.Balance += request.Amount;
            _accountRepo.Update(account);

            return new DepositResponseDto
            {
                CustomerId = request.CustomerId,
                AccountId = request.AccountId,
                Balance = account.Balance,
                Succeeded = true
            };
        }

        private static DepositResponseDto FailDeposit(string message, DepositRequestDto request)
        {
            return new DepositResponseDto
            {
                CustomerId = request.CustomerId,
                AccountId = request.AccountId,
                Balance = 0,
                Succeeded = false,
                Message = message
            };
        }

        #endregion

        #region Withdraw

        public WithdrawResponseDto Withdraw(WithdrawRequestDto request)
        {
            var account = _accountRepo.GetById(request.AccountId);
            var customer = _customerRepo.GetById(request.CustomerId);

            if (customer is null)
                return FailWithdraw("Customer does not exist.", request);

            if (account is null)
                return FailWithdraw("Account does not exist.", request);

            if (account.CustomerId != request.CustomerId)
                return FailWithdraw("Account does not belong to the specified customer.", request);

            if (account.Status != AccountStatus.Open)
                return FailWithdraw("Account is not open.", request);

            if (request.Amount <= 0)
                return FailWithdraw("Withdrawal amount must be greater than zero.", request);

            if (account.Balance - request.Amount < 0)
                return FailWithdraw("Insufficient funds. Withdrawal would bring balance below zero.", request);

            account.Balance -= request.Amount;
            _accountRepo.Update(account);

            return new WithdrawResponseDto
            {
                CustomerId = request.CustomerId,
                AccountId = request.AccountId,
                Balance = account.Balance,
                Succeeded = true
            };
        }

        private static WithdrawResponseDto FailWithdraw(string message, WithdrawRequestDto request)
        {
            return new WithdrawResponseDto
            {
                CustomerId = request.CustomerId,
                AccountId = request.AccountId,
                Balance = 0,
                Succeeded = false,
                Message = message
            };
        }

        #endregion

        #region Close Account

        public CloseAccountResponseDto CloseAccount(CloseAccountRequestDto request)
        {
            var account = _accountRepo.GetById(request.AccountId);
            var customer = _customerRepo.GetById(request.CustomerId);

            if (customer is null)
                return FailClose("Customer does not exist.", request);

            if (account is null)
                return FailClose("Account does not exist.", request);

            if (account.CustomerId != request.CustomerId)
                return FailClose("Account does not belong to the specified customer.", request);

            if (account.Balance != 0)
                return FailClose("Account can only be closed if the balance is exactly 0.", request);

            if (account.Status == AccountStatus.Closed)
                return FailClose("Account is already closed.", request);

            account.Status = AccountStatus.Closed;
            _accountRepo.Update(account);

            return new CloseAccountResponseDto
            {
                CustomerId = request.CustomerId,
                AccountId = request.AccountId,
                Succeeded = true
            };
        }

        private static CloseAccountResponseDto FailClose(string message, CloseAccountRequestDto request)
        {
            return new CloseAccountResponseDto
            {
                CustomerId = request.CustomerId,
                AccountId = request.AccountId,
                Succeeded = false,
                Message = message
            };
        }

        #endregion

        #region Create Account

        public CreateAccountResponseDto CreateAccount(CreateAccountRequestDto request)
        {
            var customer = _customerRepo.GetById(request.CustomerId);

            if (customer is null)
                return FailCreate("Customer does not exist.", request);

            if (request.InitialDeposit < 100)
                return FailCreate("Initial deposit must be at least 100.", request);

            if (!Enum.IsDefined(typeof(AccountType), request.AccountTypeId))
                return FailCreate("Invalid account type id. Valid values: 1 - Checking, 2 - Savings.", request);

            var accountType = (AccountType)request.AccountTypeId;
            var existingAccounts = _accountRepo.GetByCustomerId(request.CustomerId);

            // If this is the customer's first account, it must be savings (2)
            if (!existingAccounts.Any() && accountType != AccountType.Savings)
                return FailCreate("Customer's first account must be a Savings account (accountTypeId = 2).", request);

            var newAccount = new Account
            {
                CustomerId = request.CustomerId,
                Balance = request.InitialDeposit,
                AccountType = accountType,
                Status = AccountStatus.Open
            };

            newAccount = _accountRepo.Add(newAccount);

            return new CreateAccountResponseDto
            {
                CustomerId = request.CustomerId,
                AccountId = newAccount.AccountId,
                AccountTypeId = (int)newAccount.AccountType,
                Balance = newAccount.Balance,
                Succeeded = true
            };
        }

        private static CreateAccountResponseDto FailCreate(string message, CreateAccountRequestDto request)
        {
            return new CreateAccountResponseDto
            {
                CustomerId = request.CustomerId,
                AccountId = 0,
                AccountTypeId = request.AccountTypeId,
                Balance = 0,
                Succeeded = false,
                Message = message
            };
        }

        #endregion
    }
}
