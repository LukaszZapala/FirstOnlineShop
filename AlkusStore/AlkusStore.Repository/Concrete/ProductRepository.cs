using AlkusStore.Models.Entities;
using AlkusStore.Models.Identity;
using AlkusStore.Repository.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AlkusStore.Repository.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private WebContext _context;

        public ProductRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
