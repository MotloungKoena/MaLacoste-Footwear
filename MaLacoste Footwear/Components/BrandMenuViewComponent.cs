using MaLacoste_Footwear.Data;
using MaLacoste_Footwear.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MaLacoste_Footwear.Components
{
    public class BrandMenuViewComponent : ViewComponent
    {
        private IRepositoryWrapper _repo;
        public BrandMenuViewComponent(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke()
        {
            var model = new BrandListViewModel
            {
                Brands = _repo.Brand.FindAll(),
                SelectedBrand = (string)(RouteData?.Values["id"])
            };
            return View(model);
        }
    }
}
