using System.Text.Json;
using ShopCart.Models;
namespace ShopCart.Services;

public class CartService : ICartService
{
    private const string CART_KEY = "shopping_cart";
    private readonly ISession _session;

    public CartService(IHttpContextAccessor http)
        => _session = http.HttpContext!.Session;

    // ── Đọc giỏ từ Session ────────────────────────────────
    public ShoppingCart GetCart()
    {
        var json = _session.GetString(CART_KEY);
        return json is null
            ? new ShoppingCart()
            : JsonSerializer.Deserialize<ShoppingCart>(json)!;
    }

    // ── Thêm sản phẩm; tự gộp nếu đã có ──────────────────
    public void AddToCart(CartItem item)
    {
        var cart = GetCart();
        var existing = cart.Items
            .FirstOrDefault(x => x.ProductId == item.ProductId);

        if (existing is not null)
            existing.Quantity += item.Quantity;
        else
            cart.Items.Add(item);

        SaveCart(cart);
    }

    // ── Cập nhật số lượng ─────────────────────────────────
    public void UpdateQuantity(int productId, int quantity)
    {
        var cart = GetCart();
        var item = cart.Items
            .FirstOrDefault(x => x.ProductId == productId);
        if (item is null) return;

        if (quantity <= 0) cart.Items.Remove(item);
        else item.Quantity = quantity;

        SaveCart(cart);
    }

    // ── Xoá một item ──────────────────────────────────────
    public void RemoveFromCart(int productId)
    {
        var cart = GetCart();
        cart.Items.RemoveAll(x => x.ProductId == productId);
        SaveCart(cart);
    }

    // ── Xoá toàn bộ sau khi đặt hàng thành công ──────────
    public void ClearCart() => _session.Remove(CART_KEY);

    private void SaveCart(ShoppingCart cart)
        => _session.SetString(CART_KEY,
               JsonSerializer.Serialize(cart));
}