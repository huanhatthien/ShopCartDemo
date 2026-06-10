namespace ShopCart.Models;

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    // Tính toán — không lưu trong Session
    public decimal Total => Price * Quantity;
}