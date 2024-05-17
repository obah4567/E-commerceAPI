using System.ComponentModel.DataAnnotations;

namespace E_commerceAPI.src.Domain.Models
{
    public class Shipment
    {
        [Required]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Code_Postal { get; set; }
        public bool IsDeleted { get; set; } = false;

        /*[ForeignKey("customer")]
        public string Customer_Id { get; set; }
        public ApplicationUser customer { get; set; }*/
    }
}
