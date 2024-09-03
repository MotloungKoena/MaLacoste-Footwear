namespace MaLacoste_Footwear.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDbContext _appDbContext;
        private IBrandRepository _brand;
        private ISneakerRepository _sneaker;
        public RepositoryWrapper(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public ISneakerRepository Sneaker
        {
            get
            {
                if (_sneaker is null)
                {
                    _sneaker = new SneakerRepository(_appDbContext);
                }
                return _sneaker;
            }
        }

        public IBrandRepository Brand
        {
            get
            {
                if (_brand is null)
                {
                    _brand = new BrandRepository(_appDbContext);
                }
                return _brand;
            }
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
