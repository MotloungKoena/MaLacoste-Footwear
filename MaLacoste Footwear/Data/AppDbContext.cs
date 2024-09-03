using MaLacoste_Footwear.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaLacoste_Footwear.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Sneaker> Sneakers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Gender> Genders { get; set; }
    }
}
