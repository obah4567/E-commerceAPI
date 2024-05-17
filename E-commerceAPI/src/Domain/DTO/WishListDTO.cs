using E_commerceAPI.src.Domain.Models;

namespace E_commerceAPI.src.Domain.DTO
{
    public class WishListDTO
    {
        public int Id { get; set; }
        public int? Product_Id { get; set; }
        public Product? Product { get; set; }
    }
}
