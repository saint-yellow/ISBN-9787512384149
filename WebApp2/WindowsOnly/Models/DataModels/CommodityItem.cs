namespace WindowsOnly.Models.DataModels
{
    public class CommodityItem
    {
        public int Id { get; set; }

        public int ShoppingCartId { get; set; }

        public int BookId { get; set; }

        public int Quantity { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public virtual Book Book { get; set; }
    }
}