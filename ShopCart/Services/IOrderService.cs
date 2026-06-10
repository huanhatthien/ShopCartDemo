using ShopCart.Models;
namespace ShopCart.Services;

public interface IOrderService
{
    Order CreateOrder(Order order);
    Order? GetById(int id);
    IEnumerable<Order> GetAll();
}