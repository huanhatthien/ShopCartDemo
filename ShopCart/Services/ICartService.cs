using ShopCart.Models;
namespace ShopCart.Services;

public interface ICartService
{
    ShoppingCart GetCart();
    void AddToCart(CartItem item);
    void UpdateQuantity(int productId, int quantity);
    void RemoveFromCart(int productId);
    void ClearCart();
}