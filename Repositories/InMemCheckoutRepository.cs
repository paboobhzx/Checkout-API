using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using checkoutapi.Entities;

namespace checkoutapi.Repositories
{


    public class InMemCheckoutRepository : ICheckoutRepository
    {
        private readonly List<CheckoutItem> items = new()
        {
            new CheckoutItem { Id = Guid.NewGuid(), BillingName="Snack", Price=15, CheckoutRegisterDate = DateTimeOffset.UtcNow, IsPaid = "true"},
            new CheckoutItem { Id = Guid.NewGuid(), BillingName="Potatoes", Price=8, CheckoutRegisterDate = DateTimeOffset.UtcNow,IsPaid = "true"},
            new CheckoutItem { Id = Guid.NewGuid(), BillingName="Tomatoes", Price=20, CheckoutRegisterDate = DateTimeOffset.UtcNow,IsPaid = "false"},
            new CheckoutItem { Id = Guid.NewGuid(), BillingName="Carrots", Price=5, CheckoutRegisterDate = DateTimeOffset.UtcNow,IsPaid = "false"}
        };

        public async Task<CheckoutItem> GetCheckoutItemAsync(Guid id)
        {
            //var stuff = items.Where(items => items.Id == id).SingleOrDefault();
            return await Task.FromResult(items.Where(items => items.Id == id).SingleOrDefault());
        }
        public async Task<IEnumerable<CheckoutItem>> GetAllCheckoutItemsAsync(string status)
        {
            return await Task.FromResult(items);
        } 

        public async Task CreateCheckoutItemAsync(CheckoutItem item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateCheckoutItemAsync(CheckoutItem item)
        {
            var index = items.FindIndex(x => x.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;
            
        }

        public async Task DeleteCheckoutItemAsync(Guid id)
        {
            var index = items.FindIndex(x => x.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}
