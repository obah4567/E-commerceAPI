namespace E_commerceAPI.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
        public string Description { get; set; }

        public int? Category_Id { get; set; }
    }
}
