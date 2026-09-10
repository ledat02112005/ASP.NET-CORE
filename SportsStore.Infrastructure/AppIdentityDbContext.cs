// SportsStore.Infrastructure/AppIdentityDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsStore.Domain;

namespace SportsStore.Infrastructure;

// Kế thừa từ IdentityDbContext<AppUser> để tự động tạo các bảng Identity
// (AspNetUsers, AspNetRoles, AspNetUserRoles, AspNetUserClaims, v.v.)
public class AppIdentityDbContext : IdentityDbContext<AppUser>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options) { }
}
