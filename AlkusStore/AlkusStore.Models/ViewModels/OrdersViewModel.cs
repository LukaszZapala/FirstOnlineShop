using AlkusStore.Models.Entities;
using System.Collections.Generic;

namespace AlkusStore.Models.ViewModels
{
    public class OrdersViewModel
    {
        public IEnumerable<Orders> Orders { get; set; }

        public StatusViewModel StatusModel { get; set; }

        public string SortBy { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
