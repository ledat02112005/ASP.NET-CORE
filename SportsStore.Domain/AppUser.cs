// SportsStore.Domain/AppUser.cs
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Domain;

// Kế thừa từ IdentityUser để có thể thêm các thuộc tính tùy chỉnh
public class AppUser : IdentityUser
{
    // Có thể thêm các thuộc tính bổ sung ở đây trong tương lai
    // Ví dụ: public string? FullName { get; set; }
}
