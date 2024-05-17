namespace E_commerceAPI.src.Domain.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public int? Category_Id { get; set; }
    }
}
