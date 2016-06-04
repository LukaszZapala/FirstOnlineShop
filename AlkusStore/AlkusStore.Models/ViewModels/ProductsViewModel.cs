using AlkusStore.Models.Entities;
using System.Collections.Generic;

namespace AlkusStore.Models.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public CategoriesViewModel CategoriesModel { get; set; }

        public string SortBy { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
