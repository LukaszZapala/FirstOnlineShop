using AlkusStore.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlkusStore.Repository.Abstract
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<ShoppingCart>> GetCartAsync(string userId);

        Task<bool> AddOrUpdateAsync(string userId, int productId, int quantity);

        Task<bool> RemoveAsync(string userId, int productId);

        Task<bool> RemoveAllAsync(string userId);

        Task<Product> GetProductAsync(int id);
    }
}
