using System.Collections.Generic;

namespace AlkusStore.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public IEnumerable<string> Categories { get; set; }

        public string CurrentCategory { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }
    }
}
