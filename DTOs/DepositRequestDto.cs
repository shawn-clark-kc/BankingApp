namespace BankingApp.DTOs
{
    public class DepositRequestDto
    {
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
