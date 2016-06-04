using AlkusStore.Models.Entities;
using AlkusStore.Models.ViewModels;
using AlkusStore.Repository.Abstract;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AlkusStore.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        private IAdminRepository _repository;

        public AdminController(IAdminRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public int PageSize { get; set; } = 5;

        public async Task<ActionResult> ProductList(string category, int page = 1, string sortBy = "default")
        {
            var model = new ProductsViewModel();

            if (category == null)
            {
                model.Products = await _repository.GetProductsAsync();
            }
            else
            {
                model.Products = (await _repository.GetProductsAsync())
                    .Where(x => x.Category == category);
            }

            if (sortBy == "NameAscending")
            {
                model.Products = model.Products.OrderBy(x => x.Name);
            }
            else if (sortBy == "NameDescending")
            {
                model.Products = model.Products.OrderByDescending(x => x.Name);
            }
            else if (sortBy == "PriceAscending")
            {
                model.Products = model.Products.OrderBy(x => x.Price);
            }
            else if (sortBy == "PriceDescending")
            {
                model.Products = model.Products.OrderByDescending(x => x.Price);
            }
            else
            {
                model.Products = model.Products.OrderBy(x => x.Id);
            }

            model.Products = model.Products
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            model.SortBy = sortBy;

            model.CategoriesModel = new CategoriesViewModel()
            {
                Categories = (await _repository.GetProductsAsync())
                    .Select(x => x.Category)
                    .Distinct()
                    .OrderBy(x => x),
                CurrentCategory = category,
                Action = "ProductList",
                Controller = "Admin"
            };

            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = category == null ?
                    (await _repository.GetProductsAsync()).Count() :
                    (await _repository.GetProductsAsync()).Where(x => x.Category == category).Count()
            };

            return View(model);
        }

        public async Task<ActionResult> EditProduct(int id)
        {
            var product = await _repository.GetProductAsync(id);

            if (product == null)
            {
                ViewBag.EditError = "Nie znaleziono produktu!";
            }

            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> EditProduct(Product product)
        {
            if (product != null)
            {
                // Show error!
            }

            var result = await _repository.AddOrUpdateProductAsync(product);

            if (result == false)
            {
                // Show error!
            }

            return View(product);
        }

        public async Task<ActionResult> RemoveProduct(int id)
        {
            var result = await _repository.RemoveProductAsync(id);

            if (result == false)
            {
                // Show error
            }

            return RedirectToAction("ProductList");
        }

        public ActionResult AddProduct()
        {
            return View(new Product());
        }

        public async Task<ActionResult> SearchProduct(string key, string category = null, int page = 1, string sortBy = "default")
        {
            if (key == null) return RedirectToAction("List");

            ViewBag.Key = key;

            var model = new ProductsViewModel();

            if (category == null)
            {
                model.Products = (await _repository.GetProductsAsync())
                    .Where(x => x.Name.Contains(key) || x.Category.Contains(key));
            }
            else
            {
                model.Products = (await _repository.GetProductsAsync())
                    .Where(x => (x.Category == category) && (x.Name.Contains(key) || x.Category.Contains(key)));
            }

            if (sortBy == "NameAscending")
            {
                model.Products = model.Products.OrderBy(x => x.Name);
            }
            else if (sortBy == "NameDescending")
            {
                model.Products = model.Products.OrderByDescending(x => x.Name);
            }
            else if (sortBy == "PriceAscending")
            {
                model.Products = model.Products.OrderBy(x => x.Price);
            }
            else if (sortBy == "PriceDescending")
            {
                model.Products = model.Products.OrderByDescending(x => x.Price);
            }
            else
            {
                model.Products = model.Products.OrderBy(x => x.Id);
            }

            model.Products = model.Products
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            model.SortBy = sortBy;

            model.CategoriesModel = new CategoriesViewModel()
            {
                Categories = (await _repository.GetProductsAsync())
                    .Select(x => x.Category)
                    .Distinct()
                    .OrderBy(x => x),
                CurrentCategory = category,
                Action = "SearchProduct",
                Controller = "Admin"
            };

            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = category == null ?
                    (await _repository.GetProductsAsync())
                        .Where(x => x.Name.Contains(key) || x.Category.Contains(key)).Count() :
                    (await _repository.GetProductsAsync())
                        .Where(x => (x.Name.Contains(key) || x.Category.Contains(key)) && x.Category == category).Count()
            };

            return View(model);
        }

        public async Task<ActionResult> UserList()
        {
            return View(await _repository.GetUsersAsync());
        }

        public async Task<ActionResult> OrderList(string status, int page = 1, string sortBy = "sortBy")
        {
            var model = new OrdersViewModel();

            if (status == null)
            {
                model.Orders = await _repository.GetOrdersAsync();
            }
            else
            {
                model.Orders = (await _repository.GetOrdersAsync())
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
                List = (await _repository.GetOrdersAsync())
                    .Select(x => x.Status)
                    .Distinct()
                    .OrderBy(x => x),
                CurrentStatus = status,
                Action = "OrderList",
                Controller = "Admin"
            };

            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = status == null ?
                    (await _repository.GetOrdersAsync()).Count() :
                    (await _repository.GetOrdersAsync()).Where(x => x.Status == status).Count()
            };

            return View(model);
        }

        public async Task<ActionResult> RedeemOrder(int id, string status = "ZREALIZOWANE")
        {
            var result = await _repository.ChangeOrderStatus(id, status);
            if (result == false)
            {
                ViewBag.OrderError = "Nie udało się zmienić statusu zmówienia!";
                
            }
            return RedirectToAction("OrderList");
        }
    }
}