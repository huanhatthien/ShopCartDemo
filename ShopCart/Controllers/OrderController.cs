using Microsoft.AspNetCore.Mvc;
using ShopCart.Models;
using ShopCart.Services;
namespace ShopCart.Controllers;

public class OrderController : Controller
{
    private readonly ICartService _cart;
    private readonly IOrderService _orders;

    public OrderController(ICartService cart,
                           IOrderService orders)
    { _cart = cart; _orders = orders; }

    // GET /Order/Checkout ─────────────────────────────────
    public IActionResult Checkout()
    {
        var cart = _cart.GetCart();
        if (cart.Items.Count == 0)
        {
            TempData["Error"] = "Giỏ hàng trống.";
            return RedirectToAction("Index", "Cart");
        }
        ViewBag.Cart = cart;
        return View();
    }

    // POST /Order/Checkout ────────────────────────────────
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Checkout(string customerName,
                                  string phone,
                                  string address,
                                  string? note)
    {
        if (string.IsNullOrWhiteSpace(customerName)
         || string.IsNullOrWhiteSpace(address))
        {
            ModelState.AddModelError("",
                "Vui lòng điền đầy đủ thông tin.");
            ViewBag.Cart = _cart.GetCart();
            return View();
        }
        var cart = _cart.GetCart();
        var order = new Order
        {
            CustomerName = customerName,
            Phone = phone,
            Address = address,
            Note = note,
            TotalAmount = cart.TotalPrice,
            OrderDetails = cart.Items.Select(i => new OrderDetail
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Price = i.Price,
                Quantity = i.Quantity,
            }).ToList(),
        };
        var saved = _orders.CreateOrder(order);
        _cart.ClearCart();   // xoá session sau khi lưu OK
        return RedirectToAction(nameof(Success),
                                new { id = saved.Id });
    }

    // GET /Order/Success/{id} ─────────────────────────────
    public IActionResult Success(int id)
    {
        var order = _orders.GetById(id);
        if (order is null) return NotFound();
        return View(order);
    }
}