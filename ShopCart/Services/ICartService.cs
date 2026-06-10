using ShopCart.Models;

namespace ShopCart.Services;

public interface ICartService
{
    ShoppingCart GetCart();
    void AddToCart(CartItem item);
    void UpdateQuantity(int productId, string size, int quantity);
    void RemoveFromCart(int productId, string size);
    void ClearCart();
}