namespace MaLacoste_Footwear.Data
{
    public interface IRepositoryWrapper
    {
        ISneakerRepository Sneaker { get; }
        IBrandRepository Brand { get; }

        void Save();
    }
}
