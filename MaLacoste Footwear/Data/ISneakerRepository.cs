using MaLacoste_Footwear.Data.DataAccess;
using MaLacoste_Footwear.Models;

namespace MaLacoste_Footwear.Data
{
    public interface ISneakerRepository : IRepositoryBase<Sneaker>
    {
        IEnumerable<Sneaker> GetAllSneakersInBrand(string brand);
        IEnumerable<Sneaker> GetAllSneakersWithBrandDetails();
    }
}
