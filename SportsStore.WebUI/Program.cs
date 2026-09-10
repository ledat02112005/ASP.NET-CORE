using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using SportsStore.Domain;
using SportsStore.Infrastructure;
using SportsStore.WebUI.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------------
// 1. ĐĂNG KÝ CÁC DỊCH VỤ (SERVICES)
// -------------------------------------------------------------
builder.Services.AddControllersWithViews();

// --- CẤU HÌNH EF CORE cho SportsStore (Products) ---
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("SportsStoreConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// --- CẤU HÌNH EF CORE cho Identity ---
builder.Services.AddDbContext<AppIdentityDbContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("AppIdentityDbContextConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// --- ĐĂNG KÝ IDENTITY ---
builder.Services.AddDefaultIdentity<AppUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppIdentityDbContext>();

// Thay FakeProductRepository bằng EFProductRepository (dùng CSDL thật)
builder.Services.AddScoped<IProductRepository, EFProductRepository>();

// Đăng ký Session & Cache
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký SessionCart
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

var app = builder.Build();

// -------------------------------------------------------------
// 2. CẤU HÌNH PIPELINE XỬ LÝ REQUEST (MIDDLEWARE)
// -------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Phục vụ các file tĩnh trong wwwroot (ảnh, css, js)
app.UseStaticFiles();

app.UseRouting();

// Kích hoạt Session (Đặt sau UseRouting, trước Authorization & Route)
app.UseSession();

// Authentication phải đặt TRƯỚC Authorization
app.UseAuthentication();
app.UseAuthorization();

// Điều hướng Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Cần thiết để Identity UI (Razor Pages) hoạt động
app.MapRazorPages();

// Gọi phương thức seeding dữ liệu
SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);

app.Run();
