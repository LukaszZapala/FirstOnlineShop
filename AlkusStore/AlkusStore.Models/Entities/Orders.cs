using System;

namespace AlkusStore.Models.Entities
{
    public class Orders
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; }
    }
}
