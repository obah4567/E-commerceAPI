namespace E_commerceAPI.src.Domain.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
        public double Amount { get; set; }
    }
}
