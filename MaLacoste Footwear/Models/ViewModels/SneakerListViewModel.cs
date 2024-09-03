namespace MaLacoste_Footwear.Models.ViewModels
{
    public class SneakerListViewModel
    {
        public IEnumerable<Sneaker> Sneakers { get; set; }
        public string SelectedBrand { get; set; }
        public PagingInfoViewModel PagingInfo { get; set; }
    }
}
