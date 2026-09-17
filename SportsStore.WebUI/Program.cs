using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql;
using SportsStore.Domain;
using SportsStore.Infrastructure;
using SportsStore.WebUI.Middleware;
using SportsStore.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;

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

// --- CẤU HÌNH CORS ---
// Cho phép Angular dev server (http://localhost:4200) gọi API
builder.Services.AddCors(options => {
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// --- THÊM JWT BEARER làm scheme bổ sung (KHÔNG override default scheme của Identity) ---
// Identity đã đăng ký Cookie scheme làm mặc định (qua AddDefaultIdentity ở trên).
// Chỉ cần gọi AddJwtBearer để thêm Bearer scheme — MVC vẫn dùng Cookie, API dùng Bearer.
builder.Services.AddAuthentication()
    .AddJwtBearer(o => {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer              = builder.Configuration["Jwt:Issuer"],
            ValidAudience            = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey         = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = false, // Tạm thời tắt kiểm tra hết hạn để dễ test
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// -------------------------------------------------------------
// 2. CẤU HÌNH PIPELINE XỬ LÝ REQUEST (MIDDLEWARE)
// -------------------------------------------------------------

// Global Exception Handler - đặt đầu tiên để bắt mọi lỗi trong pipeline
app.UseMiddleware<ExceptionHandlerMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Phục vụ các file tĩnh trong wwwroot (ảnh, css, js)
app.UseStaticFiles();

// Kích hoạt CORS - đặt trước UseRouting
app.UseCors("AllowSpecificOrigin");

app.UseRouting();

// Kích hoạt Session (đặt sau UseRouting, trước Authorization)
app.UseSession();

// Authentication phải đứng TRƯỚC Authorization
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
