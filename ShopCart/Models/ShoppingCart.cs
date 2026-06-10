namespace ShopCart.Models;

public class ShoppingCart
{
    public List<CartItem> Items { get; set; } = new();
    public int TotalItems => Items.Sum(x => x.Quantity);
    public decimal TotalPrice => Items.Sum(x => x.Total);
}   