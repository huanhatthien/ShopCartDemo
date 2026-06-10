namespace ShopCart.Models;

public class OrderDetail
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal => Price * Quantity;

    // dòng này để lưu Size
    public string SelectedSize { get; set; } = "";
}