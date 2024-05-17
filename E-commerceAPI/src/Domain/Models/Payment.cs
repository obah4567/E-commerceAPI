namespace E_commerceAPI.src.Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
        public double Amount { get; set; }
        public bool IsDeleted { get; set; } = false;

        /*[ForeignKey("customer")]
        public string Customer_Id { get; set; }*/
    }
}
