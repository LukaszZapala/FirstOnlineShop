using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlkusStore.Models.Entities;
using AlkusStore.Repository.Abstract;
using AlkusStore.Models.Identity;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using AlkusStore.Models;
using System.Linq;

namespace AlkusStore.Repository.Concrete
{
    public class AdminRepository : IAdminRepository
    {
        private WebContext _context;

        public AdminRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrUpdateProductAsync(Product product)
        {
            if (product == null) return false;

            try
            {
                _context.Products.AddOrUpdate(x => x.Id, product);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<Orders> GetOrderAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Orders>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> RemoveProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null) return false;

            try
            {
                _context.Products.Remove(product);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ChangeOrderStatus(int id, string status)
        {
            var order = await GetOrderAsync(id);

            if (order == null) return false;

            order.Status = status;

            try
            {
                _context.Orders.AddOrUpdate(x => x.Id, order);

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
