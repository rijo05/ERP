using ERP.Domain.ValueObjects;
using ERP.Domain.Common;

namespace ERP.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Price Price { get; private set; }
        public bool IsActive { get; private set; }
        public Guid CategoryId { get; private set; }


        private Product() { }

        public Product(string name, Price price, Guid categoryId, string? description) 
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstTooLong(description, nameof(description), 255);

            Name = name;
            Description = description;
            Price = price;
            IsActive = true;    
            CategoryId = categoryId;
        }

        public void ChangePrice(decimal price)
        {
            Price = new Price(price);
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;

    }
}
