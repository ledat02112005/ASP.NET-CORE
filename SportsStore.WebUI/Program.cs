using SportsStore.Domain;
using SportsStore.Infrastructure;
using SportsStore.WebUI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Đăng ký IProductRepository với implementation FakeProductRepository
builder.Services.AddScoped<IProductRepository, FakeProductRepository>();

// Đăng ký bộ nhớ đệm dạng Ram (Session cần cái này để hoạt động)
builder.Services.AddDistributedMemoryCache();

// Đăng ký dịch vụ Session vào hệ thống
builder.Services.AddSession(options => {
    // Nếu khách không thao tác gì trên web trong 30 phút, giỏ hàng sẽ bị xóa
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    // Bảo mật: Không cho phép JavaScript ở trình duyệt đọc được Cookie chứa ID Session
    options.Cookie.HttpOnly = true;
    // Đánh dấu Cookie này là thiết yếu, không bị chặn bởi các quy định GDPR
    options.Cookie.IsEssential = true;
});

// Đăng ký IHttpContextAccessor để SessionCart có thể truy cập Session
builder.Services.AddHttpContextAccessor();

// Đăng ký Cart theo scope - mỗi request sẽ lấy Cart từ Session hoặc tạo mới
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Kích hoạt Session middleware - PHẢI đặt TRƯỚC UseRouting
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
