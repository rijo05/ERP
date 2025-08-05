namespace ERP.Application.DTOs.Product
{
    public class ProductResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public bool IsActive { get; set; }
        public int Stock {  get; set; }
        public int MinimumStockLevel { get; set; }
        public Dictionary<string, object> Links { get; set; }
    }
}
