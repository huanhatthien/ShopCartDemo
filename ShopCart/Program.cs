using Microsoft.AspNetCore.Cors.Infrastructure;
using ShopCart.Services; // thêm dòng này vào đầu file
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ── Session ──────────────────────────────────────────────
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

// ── Services ─────────────────────────────────────────────
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();       // ← SAU UseRouting, TRƯỚC UseAuthorization
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");
app.Run();