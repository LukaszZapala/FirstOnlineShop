using AlkusStore.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlkusStore.Models.Entities;
using AlkusStore.Models.Identity;
using System.Data.Entity;

namespace AlkusStore.Repository.Concrete
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private WebContext _context;

        public ShoppingCartRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrUpdateAsync(string userId, int productId, int quantity)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null) return false;

            var productCart = await _context.ShoppingCart.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);

            try
            {
                if (productCart == null)
                {
                    _context.ShoppingCart.Add(new ShoppingCart()
                    {
                        Price = quantity * product.Price,
                        Quantity = quantity,
                        ProductId = productId,
                        UserId = userId
                    });
                }
                else
                {
                    productCart.Quantity = (productCart.Quantity + quantity) > product.Stock ? productCart.Quantity : (productCart.Quantity + quantity);
                    productCart.Price = productCart.Quantity * product.Price;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<ShoppingCart>> GetCartAsync(string userId)
        {
            return (await _context.ShoppingCart.Where(x => x.UserId == userId).ToListAsync());
        }

        public async Task<bool> RemoveAllAsync(string userId)
        {
            var list = await _context.ShoppingCart.Where(x => x.UserId == userId).ToListAsync();

            if (list.Count() == 0) return true;

            try
            {
                foreach (var item in list)
                {
                    _context.ShoppingCart.Remove(item);
                }

                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RemoveAsync(string userId, int productId)
        {
            var product = await _context.ShoppingCart.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            if (product == null) return false;

            try
            {
                _context.ShoppingCart.Remove(product);

                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            
            return true;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
