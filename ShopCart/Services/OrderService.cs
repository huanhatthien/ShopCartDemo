using ShopCart.Models;
namespace ShopCart.Services;

// Singleton — lưu trong RAM (thay Database cho bài lab)
public class OrderService : IOrderService
{
    private readonly List<Order> _orders = new();
    private int _nextId = 1000;

    public Order CreateOrder(Order order)
    {
        order.Id = ++_nextId;
        order.CreatedDate = DateTime.Now;
        _orders.Add(order);
        return order;
    }
    public Order? GetById(int id) =>
        _orders.FirstOrDefault(o => o.Id == id);
    public IEnumerable<Order> GetAll() => _orders;
}