namespace ERP.Application.DTOs.Category
{
    public class CategoryResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public Dictionary<string, object> Links { get; set; }
    }
}
