using MaLacoste_Footwear.Models;

namespace MaLacoste_Footwear.Data
{
    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
