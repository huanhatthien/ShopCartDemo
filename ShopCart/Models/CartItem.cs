namespace ShopCart.Models;

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    // Thêm dòng này để lưu Size
    public string SelectedSize { get; set; } = "";

    public decimal Total => Price * Quantity;
}