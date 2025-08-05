using ERP.Domain.Guard;

namespace ERP.Domain.Entities;

public class Service
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }
    public TimeSpan Duration { get; private set; }

    private Service() { }

    public Service(string name, decimal price, TimeSpan duration)
    {
        GuardCommon.AgainstNullOrEmpty(name, nameof(name));
        GuardCommon.AgainstMaxLength(name, 100, nameof(name));
        GuardCommon.AgainstNull(price, nameof(price));
        GuardCommon.AgainstNegativeOrZero(price, nameof(price));
        GuardCommon.AgainstLowDuration(duration, nameof(duration));

        Name = name;
        Price = price;
        Duration = duration;
        IsActive = true;
    }

    public void ChangeActive(bool isActive)
    {
        IsActive = isActive;
    }
}
