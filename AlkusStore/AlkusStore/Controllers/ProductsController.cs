using AlkusStore.Models;
using AlkusStore.Models.ViewModels;
using AlkusStore.Repository.Abstract;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AlkusStore.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        public int PageSize { get; set; } = 5;

        public async Task<ActionResult> List(string category, int page = 1, string sortBy = "default")
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
                Action = "List",
                Controller = "Products"
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

        public async Task<ActionResult> Details(int id)
        {
            var model = (await _repository.GetProductsAsync())
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return View(model);
        }

        public async Task<ActionResult> Search(string key, string category = null, int page = 1, string sortBy = "default")
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
                Action = "Search",
                Controller = "Products"
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
    }
}