namespace BankingApp.DTOs
{
    public class DepositResponseDto
    {
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
    }
}
