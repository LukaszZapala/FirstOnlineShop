namespace AlkusStore.Models.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public string UserId { get; set; }
    }
}
