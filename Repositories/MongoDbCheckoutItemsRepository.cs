using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using checkoutapi.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace checkoutapi.Repositories
{
    public class MongoDbCheckoutItemsRepository : ICheckoutRepository
    {
        private const string dbName = "checkoutdb";
        private const string collectionName = "checkoutcollection";
        private readonly IMongoCollection<CheckoutItem> checkoutCollection;
        private readonly FilterDefinitionBuilder<CheckoutItem> filterBuilder = Builders<CheckoutItem>.Filter;

        public MongoDbCheckoutItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            checkoutCollection = database.GetCollection<CheckoutItem>(collectionName);

        } 
        public async Task CreateCheckoutItemAsync(CheckoutItem checkout)
        {            
            await checkoutCollection.InsertOneAsync(checkout);
        }

        public async Task DeleteCheckoutItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(x => x.Id, id);
            await checkoutCollection.DeleteOneAsync(filter);
        }

        public  async Task<IEnumerable<CheckoutItem>> GetAllCheckoutItemsAsync(string status)
        {
            var allItems = await checkoutCollection.Find(new BsonDocument()).ToListAsync();

            switch(status)
            {
                case "paid":
                {                    
                    return  allItems.Where(x => x.IsPaid == "true");
                }
                case "unpaid":
                {
                    return allItems.Where(x => x.IsPaid == "false");
                }
                default:
                {
                    return allItems;           
                }
            }                  
        }

        public async Task<CheckoutItem> GetCheckoutItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(x => x.Id, id);
            return await checkoutCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task UpdateCheckoutItemAsync(CheckoutItem item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id , item.Id);
            await checkoutCollection.ReplaceOneAsync(filter,item);
        }
    }
}