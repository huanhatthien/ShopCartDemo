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

    // ── Thêm sản phẩm; tự gộp nếu trùng ID VÀ trùng Size ──
    public void AddToCart(CartItem item)
    {
        var cart = GetCart();

        // Kiểm tra trùng ID và trùng Size
        var existing = cart.Items
            .FirstOrDefault(x => x.ProductId == item.ProductId && x.SelectedSize == item.SelectedSize);

        if (existing is not null)
            existing.Quantity += item.Quantity;
        else
            cart.Items.Add(item);

        SaveCart(cart);
    }

    // ── Cập nhật số lượng theo ID và Size ─────────────────
    public void UpdateQuantity(int productId, string size, int quantity)
    {
        var cart = GetCart();
        var item = cart.Items
            .FirstOrDefault(x => x.ProductId == productId && x.SelectedSize == size);

        if (item is null) return;

        if (quantity <= 0) cart.Items.Remove(item);
        else item.Quantity = quantity;

        SaveCart(cart);
    }

    // ── Xoá một item theo ID và Size ──────────────────────
    public void RemoveFromCart(int productId, string size)
    {
        var cart = GetCart();
        cart.Items.RemoveAll(x => x.ProductId == productId && x.SelectedSize == size);
        SaveCart(cart);
    }

    // ── Xoá toàn bộ sau khi đặt hàng thành công ──────────
    public void ClearCart() => _session.Remove(CART_KEY);

    private void SaveCart(ShoppingCart cart)
        => _session.SetString(CART_KEY, JsonSerializer.Serialize(cart));
}