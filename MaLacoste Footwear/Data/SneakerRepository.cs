using MaLacoste_Footwear.Data.DataAccess;
using MaLacoste_Footwear.Models;
using Microsoft.EntityFrameworkCore;

namespace MaLacoste_Footwear.Data
{
    public class SneakerRepository : RepositoryBase<Sneaker>, ISneakerRepository
    {
        public SneakerRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
        public IEnumerable<Sneaker> GetAllSneakersInBrand(string brand)
        {
            Brand cat = _appDbCOntext.Brands.FirstOrDefault(c => c.BrandName.ToLower() == brand.ToLower());

            return _appDbCOntext.Sneakers.Where(p => p.BrandId == cat.BrandId);
        }

        public IEnumerable<Sneaker> GetAllSneakersWithBrandDetails()
        {
            return _appDbCOntext.Sneakers.Include(m => m.Brand);
        }
    }
}
