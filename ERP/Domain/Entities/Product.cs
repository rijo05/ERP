using ERP.Application.DTOs.Product;
using ERP.Application.Events.Products;
using ERP.Domain.Common;
using ERP.Domain.Guard;

namespace ERP.Domain.Entities;

public class Product : IHasDomainEvents
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }
    public Guid CategoryId { get; private set; }
    public int Stock { get; private set; }
    public int ReservedStock { get; private set; }
    public int MinimumStockLevel { get; private set; }


    private readonly List<IDomainEvent> _domainEvents = new();
    public List<IDomainEvent> DomainEvents => _domainEvents;
    public void ClearDomainEvents() => _domainEvents.Clear();

    private Product() { }

    public Product(string name, decimal price, Guid categoryId, int? stock, int? minimumStockLevel, string? description) 
    {
        GuardCommon.AgainstNullOrEmpty(name, nameof(name));
        GuardCommon.AgainstMaxLength(name, 100, nameof(name));
        GuardCommon.AgainstNull(price, nameof(price));
        GuardCommon.AgainstNegativeOrZero(price, nameof(price));
        GuardCommon.AgainstNegative(stock ?? 0, nameof(stock));
        GuardCommon.AgainstNegative(minimumStockLevel ?? 10, nameof(minimumStockLevel));

        if (categoryId == Guid.Empty)
            throw new ArgumentException("CategoryId cannot be empty GUID.", nameof(categoryId));

        Name = name;
        Description = description;
        Price = price;
        IsActive = true;
        Stock = stock ?? 0;
        MinimumStockLevel = minimumStockLevel ?? 10;
        CategoryId = categoryId;
    }

    public void UpdateFromDTO(UpdateProductDTO dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.Name))
            UpdateName(dto.Name);

        if (dto.Description is not null)
        {
            if (dto.Description == "")
                ClearDescription();
            else
                UpdateDescription(dto.Description);
        }

        if (dto.Price.HasValue)
            ChangePrice(dto.Price.Value);

        if (!string.IsNullOrWhiteSpace(dto.CategoryId))
        {
            if (Guid.TryParse(dto.CategoryId, out var newCategoryId) && newCategoryId != CategoryId)
                UpdateCategory(newCategoryId);
            else
                throw new ArgumentException("Invalid CategoryId GUID format.");
        }

        if (dto.IsActive.HasValue)
            ChangeActive(dto.IsActive.Value);
    }

    private void UpdateName(string newName)
    {
        GuardCommon.AgainstNullOrEmpty(newName, nameof(newName));
        GuardCommon.AgainstMaxLength(newName, 100, nameof(newName));
        Name = newName;
    }

    private void UpdateDescription(string description)
    {
        GuardCommon.AgainstMaxLength(description, 255, nameof(description));
        Description = description;
    }

    private void ChangePrice(decimal newPrice)
    {
        GuardCommon.AgainstNegativeOrZero(newPrice, nameof(newPrice));
        Price = newPrice;
    }

    private void UpdateCategory(Guid newCategoryId)
    {
        if (newCategoryId == Guid.Empty)
            throw new ArgumentException("CategoryId cannot be empty GUID.", nameof(newCategoryId));

        CategoryId = newCategoryId;
    }

    private void ChangeActive(bool isActive)
    {
        IsActive = isActive;
    }

    private void ClearDescription()
    {
        Description = null;
    }

    public void ChangeStock(int quantity)
    {
        if (quantity < 0)
        {
            var absQuantidade = Math.Abs(quantity);
            if (absQuantidade > this.Stock)
                throw new InvalidOperationException("Not enough stock");
        }

        Stock += quantity;

        TriggerDomainEvents(Stock);
    }

    public void SetStock(int quantity)
    {
        GuardCommon.AgainstNegative(quantity, nameof(quantity));
        Stock = quantity;

        TriggerDomainEvents(Stock);
    }

    public void ChangeMinimumStockLevel(int quantity)
    {
        if (quantity < 0)
            throw new Exception("Quantity must be greater or equal to zero");

        this.MinimumStockLevel = quantity;
    }

    public void UpdateInventoryFromDTO(UpdateProductInventoryDTO inventoryDTO)
    {
        if(inventoryDTO.Stock is not null) SetStock(inventoryDTO.Stock.Value);

        if (inventoryDTO.MinimumStockLevel is not null) ChangeMinimumStockLevel(inventoryDTO.MinimumStockLevel.Value);
    }

    private void TriggerDomainEvents(int stock)
    {
        if (stock == 0)
            _domainEvents.Add(new ProductOutOfStockEvent(Id));
        else if (stock <= MinimumStockLevel)
            _domainEvents.Add(new ProductStockLowEvent(Id));
    }
}
