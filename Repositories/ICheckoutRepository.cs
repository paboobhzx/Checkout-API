using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using checkoutapi.Entities;

namespace checkoutapi.Repositories
{
        public interface ICheckoutRepository
    {
        Task<CheckoutItem> GetCheckoutItemAsync(Guid id);
        Task<IEnumerable<CheckoutItem>> GetAllCheckoutItemsAsync(string status);        
        Task CreateCheckoutItemAsync(CheckoutItem item);
        Task UpdateCheckoutItemAsync(CheckoutItem item);          
        Task DeleteCheckoutItemAsync(Guid id);      

    }
}