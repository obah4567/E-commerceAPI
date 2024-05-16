using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceAPI.DTO;

public class CartDTO
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int Product_Id { get; set; }

}
