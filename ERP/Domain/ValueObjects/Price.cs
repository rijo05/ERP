using ERP.Domain.Common;

namespace ERP.Domain.ValueObjects
{
    public class Price
    {
        public decimal Amount { get; private set; }
        public Price(decimal price)
        {
            Guard.AgainstNegativeOrZero(price, nameof(price));
            Amount = price;
        }

    }
}
