using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceAPI.src.Domain.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public double Price { get; set; }

        public List<Cart>? Carts { get; set; }

        public List<WishList>? WishLists { get; set; }

        [ForeignKey("category")]
        public int? Category_Id { get; set; }
        public Category? Category { get; set; }
    }
}
