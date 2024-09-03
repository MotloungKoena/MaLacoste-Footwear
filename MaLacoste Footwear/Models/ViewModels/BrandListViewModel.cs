namespace MaLacoste_Footwear.Models.ViewModels
{
    public class BrandListViewModel
    {
        public IEnumerable<Brand> Brands { get; set; }
        public string SelectedBrand { get; set; }
        public string CheckActiveBrand(string brand) =>
            brand == SelectedBrand ? "active" : "";
    }
}
