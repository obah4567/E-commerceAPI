using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceAPI.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
        public Double Amount { get; set; }
        public bool IsDeleted { get; set; } = false;

        /*[ForeignKey("customer")]
        public string Customer_Id { get; set; }*/
    }
}
