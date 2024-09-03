using MaLacoste_Footwear.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaLacoste_Footwear.Data
{
    public class SneakerIdentityDbContext : IdentityDbContext<AppUser>
    {
        public SneakerIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
