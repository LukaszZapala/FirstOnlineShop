using AlkusStore.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlkusStore.Repository.Abstract
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductAsync(int id);
    }
}
