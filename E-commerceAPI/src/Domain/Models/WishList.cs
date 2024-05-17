using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceAPI.src.Domain.Models
{
    public class WishList
    {
        [Required]
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("product")]
        public int? Product_Id { get; set; }
        public Product? Product { get; set; }
    }
}
