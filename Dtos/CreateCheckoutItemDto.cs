using System.ComponentModel.DataAnnotations;

namespace checkoutapi.Dtos

{
    public record CreateCheckoutItemDto
    {
        [Required]
        public string BillingName {get; init;  }
        [Required]
        [Range(1,1000)]
        public decimal Price {get; init;  }
        public string IsPaid {get; init;}

    }
}