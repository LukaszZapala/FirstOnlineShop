using AlkusStore.Models.Entities;
using AlkusStore.Models.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlkusStore.Repository.Abstract
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductAsync(int id);

        Task<bool> AddOrUpdateProductAsync(Product product);

        Task<bool> RemoveProductAsync(int id);

        Task<IEnumerable<ApplicationUser>> GetUsersAsync();

        Task<ApplicationUser> GetUserAsync(string id);

        Task<IEnumerable<Orders>> GetOrdersAsync();

        Task<Orders> GetOrderAsync(int id);

        Task<bool> ChangeOrderStatus(int id, string status);
    }
}
