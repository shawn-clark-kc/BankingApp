namespace BankingApp.DTOs
{
    public class CreateAccountRequestDto
    {
        public int CustomerId { get; set; }
        public decimal InitialDeposit { get; set; }
        public int AccountTypeId { get; set; }  // 1 - Checking, 2 - Savings
    }
}
