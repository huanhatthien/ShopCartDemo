using ShopCart.Models;
namespace ShopCart.Services;

public interface IProductService
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
}