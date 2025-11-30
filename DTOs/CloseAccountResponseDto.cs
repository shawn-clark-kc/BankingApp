namespace BankingApp.DTOs
{
    public class CloseAccountResponseDto
    {
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
    }
}
