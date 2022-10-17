using System;

namespace checkoutapi.Entities
{
    public record CheckoutItem
    {
        public Guid Id {get; init;  }
        public string BillingName {get; init;  }
        public decimal Price {get; init;  }
        public DateTimeOffset CheckoutRegisterDate {get; init;  }
        public string IsPaid {get; init;}

    }
}