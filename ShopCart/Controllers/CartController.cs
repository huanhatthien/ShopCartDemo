using Microsoft.AspNetCore.Mvc;
using ShopCart.Models;
using ShopCart.Services;

namespace ShopCart.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cart;
    private readonly IProductService _products;

    public CartController(ICartService cart, IProductService products)
    { _cart = cart; _products = products; }

    // GET /Cart ───────────────────────────────────────────
    public IActionResult Index() => View(_cart.GetCart());

    // POST /Cart/AddToCart ────────────────────────────────
    [HttpPost]
    public IActionResult AddToCart(int id, int quantity = 1, string selectedSize = "", string? returnUrl = null)
    {
        var product = _products.GetById(id);
        if (product is null) return NotFound();

        // LOGIC "TRÁI TIM ĐỘC NHẤT"
        if (product.Name == "My Heart")
        {
            var cart = _cart.GetCart();
            var existing = cart.Items.FirstOrDefault(x => x.ProductId == id);

            // Nếu khách cố tình đổi html nhập số > 1, hoặc trong giỏ đã có rồi mà bấm thêm
            if (existing != null || quantity > 1)
            {
                TempData["HeartMessage"] = "Trái tim anh chỉ có 1 ❤️";
                return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
            }
        }

        _cart.AddToCart(new CartItem
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Price = product.Price,
            Quantity = quantity,
            SelectedSize = selectedSize ?? ""
        });
        return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
    }

    // POST /Cart/UpdateQuantity ───────────────────────────
    [HttpPost]
    public IActionResult UpdateQuantity(int id, string size, int quantity)
    {
        var product = _products.GetById(id);

        // Chặn nút dấu "+" ở trong giỏ hàng
        if (product != null && product.Name == "My Heart" && quantity > 1)
        {
            TempData["HeartMessage"] = "Trái tim anh chỉ có 1 ❤️";
            return RedirectToAction(nameof(Index));
        }

        _cart.UpdateQuantity(id, size ?? "", quantity);
        return RedirectToAction(nameof(Index));
    }

    // POST /Cart/Remove ───────────────────────────────────
    [HttpPost]
    public IActionResult Remove(int id, string size)
    {
        _cart.RemoveFromCart(id, size ?? "");
        return RedirectToAction(nameof(Index));
    }
}