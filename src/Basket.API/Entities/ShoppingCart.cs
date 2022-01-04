namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Itens { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart() {}
        public ShoppingCart(string userName)
        {
            UserName = UserName;
        }

        public double TotalPrice
        {
            get
            {
                double totalPrice = 0;
                foreach (var item in Itens)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
