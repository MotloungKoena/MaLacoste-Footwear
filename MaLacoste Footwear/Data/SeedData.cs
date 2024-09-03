using MaLacoste_Footwear.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MaLacoste_Footwear.Data
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Genders.Any())
            {
                context.Genders.AddRange(
                new Gender { Name = "Male" },
                new Gender { Name = "Female" },
                new Gender { Name = "Non-binary" },
                new Gender { Name = "Other" }
                );
            }
            context.SaveChanges();

            if (!context.Brands.Any())
            {
                context.Brands.AddRange(
                    new Brand { BrandName = "Carvella" },
                    new Brand { BrandName = "Nike" },
                    new Brand { BrandName = "Vans" },
                    new Brand { BrandName = "Adidas" },
                    new Brand { BrandName = "Converse" }
                    );
            }

            context.SaveChanges();

            if (!context.Sneakers.Any())
            {
                context.Sneakers.AddRange(
                    new Sneaker
                    {
                        BrandId = 1,
                        Code = "carvellablue",
                        Name = "Carvella (Blue)",
                        Price = (decimal)2200.00
                    },
                    new Sneaker
                    {
                        BrandId = 1,
                        Code = "carvellablack",
                        Name = "Carvella Suede (Black)",
                        Price = (decimal)1299.00
                    },
                    new Sneaker
                    {
                        BrandId = 1,
                        Code = "carvellafull",
                        Name = "Carvella (High Ankle, Navy)",
                        Price = (decimal)1199.00
                    },
                    new Sneaker
                    {
                        BrandId = 1,
                        Code = "carvella",
                        Name = "Carvella (Black)",
                        Price = (decimal)999.00
                    },
                    new Sneaker
                    {
                        BrandId = 2,
                        Code = "airforcewhite",
                        Name = "Airforce 1 (White)",
                        Price = (decimal)900.00
                    },
                    new Sneaker
                    {
                        BrandId = 2,
                        Code = "airforcecustom",
                        Name = "Airforce 1 (White_Custom Made)",
                        Price = (decimal)489.99
                    },
                    new Sneaker
                    {
                        BrandId = 2,
                        Code = "airforceblack",
                        Name = "Airfoce(Black)",
                        Price = (decimal)799.00
                    },
                    new Sneaker
                    {
                        BrandId = 3,
                        Code = "vansoldskool",
                        Name = "Vans Old Skool",
                        Price = (decimal)415.00
                    },
                    new Sneaker
                    {
                        BrandId = 3,
                        Code = "vansoriginal",
                        Name = "Original Vans",
                        Price = (decimal)799.99
                    },
                    new Sneaker
                    {
                        BrandId = 3,
                        Code = "vansfull",
                        Name = "Vans Old Skool(High Ankle)",
                        Price = (decimal)999.99
                    },
                    new Sneaker
                    {
                        BrandId = 4,
                        Code = "adidassandals",
                        Name = "Adidas Adillete Sandals",
                        Price = (decimal)399.99
                    },
                    new Sneaker
                    {
                        BrandId = 4,
                        Code = "adidastorsion",
                        Name = "Adidas Torsion",
                        Price = (decimal)1499.99
                    },
                    new Sneaker
                    {
                        BrandId = 5,
                        Code = "converselow",
                        Name = "Converse All Star (Black)",
                        Price = (decimal)599.99
                    },
                    new Sneaker
                    {
                        BrandId = 5,
                        Code = "conversehigh1",
                        Name = "Converse All Star (High Ankle_Black)",
                        Price = (decimal)599.99
                    },
                    new Sneaker
                    {
                        BrandId = 5,
                        Code = "conversehigh2",
                        Name = "Converse All Star (High Ankle_Black)",
                        Price = (decimal)799.99
                    },
                    new Sneaker
                    {
                        BrandId = 5,
                        Code = "conversesandals",
                        Name = "Converse All Star Sandals",
                        Price = (decimal)299.99
                    }       
                    );
            }
            context.SaveChanges();
        }
    }
}
