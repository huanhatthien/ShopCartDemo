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

        _cart.AddToCart(new CartItem
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Price = product.Price,
            Quantity = quantity,
            SelectedSize = selectedSize // Gán size vào đây
        });

        return Redirect(returnUrl ?? Url.Action("Index", "Product")!);
    }

    // POST /Cart/UpdateQuantity ───────────────────────────
    [HttpPost]
    public IActionResult UpdateQuantity(int id, int quantity)
    {
        _cart.UpdateQuantity(id, quantity);
        return RedirectToAction(nameof(Index));
    }

    // POST /Cart/Remove ───────────────────────────────────
    [HttpPost]
    public IActionResult Remove(int id)
    {
        _cart.RemoveFromCart(id);
        return RedirectToAction(nameof(Index));
    }
}