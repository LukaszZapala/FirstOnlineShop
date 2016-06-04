using AlkusStore.Models;
using AlkusStore.Models.ViewModels;
using AlkusStore.Repository.Abstract;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AlkusStore.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private IShoppingCartRepository _repository;

        public ShoppingCartController(IShoppingCartRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult> List()
        {
            var model = new ShoppingCartViewModel();

            var shoppingCart = await _repository.GetCartAsync(User.Identity.GetUserId());

            foreach (var item in shoppingCart)
            {
                var product = await _repository.GetProductAsync(item.ProductId);
                product.Price = item.Price;
                model.Products.Add(new ProductCart()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Stock = product.Stock,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    ImagePath = product.ImagePath
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(int productId, int quantity = 1)
        {
            if (quantity != 0)
            {
                await _repository.AddOrUpdateAsync(User.Identity.GetUserId(), productId, quantity);
            }
            else
            {
                await _repository.RemoveAsync(User.Identity.GetUserId(), productId);
            }

            return RedirectToAction("List");
        }

        public async Task<ActionResult> Clear()
        {
            await _repository.RemoveAllAsync(User.Identity.GetUserId());

            return RedirectToAction("List");
        }
    }
}