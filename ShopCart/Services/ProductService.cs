using ShopCart.Models;
namespace ShopCart.Services;

public class ProductService : IProductService
{
    private static readonly List<Product> _products =
    [
        new() { Id=1, Emoji="👕", Name="Áo thun",
                 Category="Thời trang", Price=150_000,
                 Description="Size S/M/L/XL" },
        new() { Id=2, Emoji="👟", Name="Giày sneaker",
                 Category="Giày dép",   Price=890_000,
                 Description="Đế cao su chống trơn" },
        new() { Id=3, Emoji="🎒", Name="Balo laptop 15.6\"",
                 Category="Phụ kiện",  Price=350_000,
                 Description="Chống thấm nước" },
        new() { Id=4, Emoji="📗", Name="Sách ASP.NET Core",
                 Category="Sách",       Price=220_000,
                 Description="Từ cơ bản đến nâng cao" },
        new() { Id=5, Emoji="❤️", Name="My Heart",
                 Category="Love",  Price=180_000,
                 Description="2.4GHz, dùng pin AA" },
        new() { Id=6, Emoji="🧢", Name="Nón kết",
                 Category="Thời trang", Price=120_000,
                 Description="Thêu logo" },
    ];
    public IEnumerable<Product> GetAll() => _products;
    public Product? GetById(int id) =>
        _products.FirstOrDefault(p => p.Id == id);
}