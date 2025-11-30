using BankingApp.Domain.Entities;
using BankingApp.Domain.Enums;

namespace BankingApp.Repositories
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly List<Account> _accounts;
        private int _nextAccountId;

        public InMemoryAccountRepository()
        {
            _accounts = new List<Account>
            {
                new Account
                {
                    AccountId = 17,
                    CustomerId = 5,
                    Balance = 2175.13m,
                    AccountType = AccountType.Checking,
                    Status = AccountStatus.Open
                }
            };

            _nextAccountId = 18;
        }

        public Account? GetById(int accountId)
        {
            return _accounts.FirstOrDefault(a => a.AccountId == accountId);
        }

        public IEnumerable<Account> GetByCustomerId(int customerId)
        {
            return _accounts.Where(a => a.CustomerId == customerId);
        }

        public Account Add(Account account)
        {
            account.AccountId = _nextAccountId++;
            _accounts.Add(account);
            return account;
        }

        public void Update(Account account)
        {
            // No-op for in-memory; tracked by reference
        }
    }
}
