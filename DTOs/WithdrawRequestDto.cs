namespace BankingApp.DTOs
{
    public class WithdrawRequestDto
    {
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
