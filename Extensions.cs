using checkoutapi.Dtos;
using checkoutapi.Entities;

namespace checkoutapi
{
    public static class Extensions
    {
        public static CheckoutItemDto AsDto(this CheckoutItem item)
        {
            return new CheckoutItemDto
            {
                Id = item.Id,
                BillingName = item.BillingName,
                CheckoutRegisterDate = item.CheckoutRegisterDate,
                IsPaid = item.IsPaid,
                Price = item.Price
            };
        }


    }
}