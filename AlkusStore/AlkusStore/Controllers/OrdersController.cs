using AlkusStore.Repository.Abstract;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AlkusStore.Models.Entities;
using System;
using System.Linq;
using AlkusStore.Models.ViewModels;

namespace AlkusStore.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private IOrdersRepository _repository;

        public OrdersController(IOrdersRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult> Order()
        {
            var cart = await _repository.GetCartListAsync(User.Identity.GetUserId());

            if (cart == null || cart.Count() == 0) return RedirectToAction("List", "ShoppingCart");

            var order = new Orders()
            {
                UserId = User.Identity.GetUserId(),
                Date = DateTime.Now,
                Status = "OCZEKUJĄCE"
            };

            foreach (var item in cart)
            {
                var product = await _repository.GetProductAsync(item.ProductId);
                order.Name += item.Quantity + " x " + product.Name + ", ";
                order.Price += item.Price;

                await _repository.ReduceProductStack(product.Id, item.Quantity);
            }

            await _repository.AddOrderAsync(order);

            await _repository.ClearCartAsync(order.UserId);

            return View(order);
        }
        
        public int PageSize { get; set; } = 5;

        public async Task<ActionResult> List(string status, int page = 1, string sortBy = "default")
        { 
            var model = new OrdersViewModel();

            if (status == null)
            {
                model.Orders = await _repository.GetOrdersListAsync(User.Identity.GetUserId());
            }
            else
            {
                model.Orders = (await _repository.GetOrdersListAsync(User.Identity.GetUserId()))
                    .Where(x => x.Status == status);
            }

            if (sortBy == "DateAscending")
            {
                model.Orders = model.Orders.OrderBy(x => x.Date);
            }
            else if (sortBy == "DateDescending")
            {
                model.Orders = model.Orders.OrderByDescending(x => x.Date);
            }
            else if (sortBy == "PriceAscending")
            {
                model.Orders = model.Orders.OrderBy(x => x.Price);
            }
            else if (sortBy == "PriceDescending")
            {
                model.Orders = model.Orders.OrderByDescending(x => x.Price);
            }
            else if (sortBy == "StatusAscending")
            {
                model.Orders = model.Orders.OrderBy(x => x.Status);
            }
            else if (sortBy == "StatusDescending")
            {
                model.Orders = model.Orders.OrderByDescending(x => x.Status);
            }
            else
            {
                model.Orders = model.Orders.OrderBy(x => x.Id);
            }

            model.Orders = model.Orders
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            model.SortBy = sortBy;

            model.StatusModel = new StatusViewModel()
            {
                List = (await _repository.GetOrdersListAsync(User.Identity.GetUserId()))
                    .Select(x => x.Status)
                    .Distinct()
                    .OrderBy(x => x),
                CurrentStatus = status,
                Action = "List",
                Controller = "Orders"
            };

            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = (await _repository.GetOrdersListAsync(User.Identity.GetUserId())).Count()
            };

            return View(model);
        }
    }
}