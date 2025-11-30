using BankingApp.Domain.Entities;

namespace BankingApp.Repositories
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private static readonly List<Customer> Customers = new()
        {
            new Customer { CustomerId = 5, FullName = "Jane Doe" }
        };

        public Customer? GetById(int customerId)
        {
            return Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }
    }
}
