namespace E_commerceAPI.src.Domain.DTO
{
    public class ShipmentDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Code_Postal { get; set; }
    }
}
