using AlkusStore.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlkusStore.Repository.Abstract
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Orders>> GetOrdersListAsync(string userId);

        Task<bool> AddOrderAsync(Orders orders);

        Task<IEnumerable<ShoppingCart>> GetCartListAsync(string userId);

        Task<Product> GetProductAsync(int id);

        Task<bool> ClearCartAsync(string userId);

        Task<bool> ReduceProductStack(int id, int quantity);
    }
}
