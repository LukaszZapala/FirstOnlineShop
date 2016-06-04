using System.Collections.Generic;

namespace AlkusStore.Models.ViewModels
{
    public class StatusViewModel
    {
        public IEnumerable<string> List { get; set; }

        public string CurrentStatus { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }
    }
}
