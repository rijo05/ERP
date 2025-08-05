using ERP.Application.DTOs.Category;
using ERP.Application.DTOs.User;
using ERP.Domain.Guard;
using ERP.Domain.ValueObjects;
using Humanizer;

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
            GuardCommon.AgainstNullOrEmpty(name, nameof(name));
            GuardCommon.AgainstMaxLength(description, 255, nameof(description));

            Name = name;
            Description = description;
            IsActive = true;
        }

        public void UpdateFromDTO(UpdateCategoryDTO categoryDTO)
        {
            if (!string.IsNullOrWhiteSpace(categoryDTO.Name))
            {
                UpdateName(categoryDTO.Name);
            }

            if (categoryDTO.Description is not null)
            {
                if (categoryDTO.Description == "")
                    ClearDescription();
                else
                    UpdateDescription(categoryDTO.Description);
            }

            if (categoryDTO.IsActive.HasValue)
            {
                SetActive(categoryDTO.IsActive.Value);
            }
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

        private void ClearDescription()
        {
            Description = null;
        }

        private void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

    }
}
