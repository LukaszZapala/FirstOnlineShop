using System.Collections.Generic;

namespace AlkusStore.Models.ViewModels
{
    public class ShoppingCartViewModel
    {

        public ShoppingCartViewModel()
        {
            Products = new List<ProductCart>();
        }

        public List<ProductCart> Products { get; set; }

        public decimal Price
        {
            get
            {
                decimal sum = 0;
                foreach (var item in Products)
                {
                    sum += item.Price;
                }
                return sum;
            }
        }
    }
}
