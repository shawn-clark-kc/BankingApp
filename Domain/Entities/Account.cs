using BankingApp.Domain.Enums;

namespace BankingApp.Domain.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }

        public decimal Balance { get; set; }

        public AccountStatus Status { get; set; } = AccountStatus.Open;

        public AccountType AccountType { get; set; }
    }
}
