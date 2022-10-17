using Microsoft.AspNetCore.Mvc;
using checkoutapi.Repositories;
using System.Collections.Generic;
using checkoutapi.Entities;
using System;
using System.Linq;
using checkoutapi.Dtos;
using System.Threading.Tasks;

namespace checkoutapi.Controllers
{

    
    [ApiController]
    [Route("checkoutitems")]
    public class CheckoutItemsController: ControllerBase
    {
        private readonly ICheckoutRepository repository;
        public CheckoutItemsController(ICheckoutRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]      
        //GET /checkoutitems
        public async Task<IEnumerable<CheckoutItemDto>> GetAllCheckoutAsync(string status)
        {
            switch(status)
            {
                case "paid":
                {
                    var paidItems = (await repository.GetAllCheckoutItemsAsync(status)).Where(x => x.IsPaid == "true").Select(y => y.AsDto());
                    return paidItems;
                }
                case "unpaid":
                {
                    var unpaidItems = (await repository.GetAllCheckoutItemsAsync(status)).Where(x => x.IsPaid == "false").Select(y => y.AsDto());
                    return unpaidItems;
                }
                default:
                {
                    return (await repository.GetAllCheckoutItemsAsync(status)).Select(x => x.AsDto());            
                }
            }      
    
        }  

        //GET /checkoutitems/id
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckoutItemDto>> GetItem(Guid id)
        {
            
            var item = await repository.GetCheckoutItemAsync(id);
            if(item is null)
            {
                return NotFound();
            }
            return  item.AsDto();
        }
        //POST /checkoutitems
        [HttpPost]
        public async Task<ActionResult<CheckoutItemDto>> CreateCheckoutDto(CreateCheckoutItemDto item)
        {
            CheckoutItem checkout = new()
            {
                Id = Guid.NewGuid(),
                BillingName = item.BillingName,
                Price = item.Price,
                IsPaid = item.IsPaid,
                CheckoutRegisterDate = DateTimeOffset.UtcNow
            };
          
            await Task.FromResult(repository.CreateCheckoutItemAsync(checkout));
            return CreatedAtAction(nameof(GetItem), new { id = checkout.Id},checkout.AsDto());
        }
        // PUT /checkoutitems/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCheckout(Guid id, UpdateCheckoutItemDto itemDto)
        {
            var currCheckout = await repository.GetCheckoutItemAsync(id);
            if(currCheckout is null)
            {
                return NotFound();
            }

            CheckoutItem updated  = currCheckout with {
                BillingName = itemDto.BillingName,
                Price = itemDto.Price,
                IsPaid = itemDto.IsPaid
            };
            await repository.UpdateCheckoutItemAsync(updated);
            return NoContent();

        }

        //DELETE /checkoutitems
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var currCheckout = await repository.GetCheckoutItemAsync(id);
            if(currCheckout is null)
            {
                return NotFound();
            }
            await repository.DeleteCheckoutItemAsync(id);
            return NoContent();
        }
    }
}