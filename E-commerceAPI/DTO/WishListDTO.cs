using E_commerceAPI.Models;

namespace E_commerceAPI.DTO
{
    public class WishListDTO
    {
        public int Id { get; set; }
        public int? Product_Id { get; set; }
        public Product? Product { get; set; }
    }
}
