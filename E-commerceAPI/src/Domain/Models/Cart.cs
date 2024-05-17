using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceAPI.src.Domain.Models
{
    public class Cart
    {
        [Required]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; } = false;

        // La classe Cart dispose d'une propriété de dependence vers Product
        /// <summary>
        /// https://www.tektutorialshub.com/entity-framework/ef-data-annotations-foreignkey-attribute/
        /// </summary>

        [ForeignKey("product")]
        public int Product_Id { get; set; }
        public Product? Product { get; set; }
    }
}
