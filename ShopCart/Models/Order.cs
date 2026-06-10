namespace ShopCart.Models;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Address { get; set; } = "";
    public string? Note { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string Status { get; set; } = "Pending";
    public decimal TotalAmount { get; set; }
    public List<OrderDetail> OrderDetails { get; set; } = new();
}