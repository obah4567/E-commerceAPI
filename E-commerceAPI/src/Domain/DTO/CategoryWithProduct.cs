namespace E_commerceAPI.src.Domain.DTO
{
    public class CategoryWithProduct
    {
        public int Id { get; set; }

        public string Category_Name { get; set; }

        public List<string>? ProductNames { get; set; }
    }
}
