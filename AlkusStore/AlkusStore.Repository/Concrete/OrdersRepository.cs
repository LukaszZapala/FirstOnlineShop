using System;
using System.Collections.Generic;
using AlkusStore.Models.Entities;
using AlkusStore.Models.Identity;
using AlkusStore.Repository.Abstract;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace AlkusStore.Repository.Concrete
{
    public class OrdersRepository : IOrdersRepository
    {
        private WebContext _context;

        public OrdersRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrderAsync(Orders orders)
        {
            try
            {
                _context.Orders.Add(orders);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Orders>> GetOrdersListAsync(string userId)
        {
            return await _context.Orders.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<ShoppingCart>> GetCartListAsync(string userId)
        {
            return (await _context.ShoppingCart.Where(x => x.UserId == userId).ToListAsync());
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ClearCartAsync(string userId)
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
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ReduceProductStack(int id, int quantity)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null || (product.Stock - quantity) < 0) return false;

            try
            {
                product.Stock -= quantity;

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
