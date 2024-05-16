using System.ComponentModel.DataAnnotations;

namespace E_commerceAPI.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
