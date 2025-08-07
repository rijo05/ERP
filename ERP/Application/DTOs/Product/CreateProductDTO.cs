namespace ERP.Application.DTOs.Product;

public class CreateProductDTO
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string CategoryId { get; set; }
    public int? Stock { get; set; }
    public int? MinimumStockLevel { get; set; }
}
