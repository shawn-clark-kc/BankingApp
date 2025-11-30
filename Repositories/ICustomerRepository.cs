using BankingApp.Domain.Entities;

namespace BankingApp.Repositories
{
    public interface ICustomerRepository
    {
        Customer? GetById(int customerId);
    }
}
