using BankingApp.Domain.Entities;

namespace BankingApp.Repositories
{
    public interface IAccountRepository
    {
        Account? GetById(int accountId);
        IEnumerable<Account> GetByCustomerId(int customerId);
        Account Add(Account account);
        void Update(Account account);
    }
}
