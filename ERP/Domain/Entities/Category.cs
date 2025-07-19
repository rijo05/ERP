using ERP.Domain.Common;

namespace ERP.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }       
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public bool IsActive { get; private set; }


        private Category() { }

        public Category(string name, string? description) 
        { 
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstTooLong(description, nameof(description), 255);

            Name = name;
            Description = description;
        }

    }
}
