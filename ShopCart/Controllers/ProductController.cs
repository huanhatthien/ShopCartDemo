using Microsoft.AspNetCore.Mvc;
using ShopCart.Services;
namespace ShopCart.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _products;
    public ProductController(IProductService products)
        => _products = products;

    public IActionResult Index()
        => View(_products.GetAll());
}