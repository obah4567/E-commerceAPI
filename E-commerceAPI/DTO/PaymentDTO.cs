using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceAPI.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
        public Double Amount { get; set; }
    }
}
