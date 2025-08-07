namespace ERP.Application.DTOs.Category;

public class CategoryResponseDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; }
    public Dictionary<string, object> Links { get; init; }
}
