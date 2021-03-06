﻿namespace AlkusStore.Models.ViewModels
{
    public class ProductCart
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string ImagePath { get; set; }

        public int Quantity { get; set; }
    }
}
