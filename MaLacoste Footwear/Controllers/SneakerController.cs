using MaLacoste_Footwear.Data;
using MaLacoste_Footwear.Data.DataAccess;
using MaLacoste_Footwear.Models;
using MaLacoste_Footwear.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;

namespace MaLacoste_Footwear.Controllers
{
    public class SneakerController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        public SneakerController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public int iPageSize = 3;
        public IActionResult List(string id = "all", string sortBy = "name", int productPage = 1) 
        {
            IEnumerable<Sneaker> sneakers; ;
            Expression<Func<Sneaker, object>> orderBy;
            string orderByDirection;
            int iTotalSneakers;

            ViewData["NameSortParam"] = sortBy == "name" ? "name_desc" : "name";
            ViewData["PriceSortParam"] = sortBy == "price" ? "price_desc" : "price";

            if (sortBy.EndsWith("_desc"))
            {
                sortBy = sortBy.Substring(0, sortBy.Length - 5);
                orderByDirection = "desc";
            }
            else
            {
                orderByDirection = "asc";
            }

            string sPropertyName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sortBy);
            orderBy = p => EF.Property<object>(p, sPropertyName);  //e.g p =>p.Name

            if (id == "all")
            {
                iTotalSneakers = _repo.Sneaker.FindAll().Count();
                sneakers = _repo.Sneaker.GetWithOptions(new QueryOptions<Sneaker>
                {
                    OrderBy = orderBy,
                    OrderByDirection = orderByDirection,
                    PageNumber = productPage,
                    PageSize = iPageSize
                });
            }
            else
            {
                int iBrandId = _repo.Brand.FindByCondition(c => c.BrandName.ToLower() == id)
                    .FirstOrDefault().BrandId;
                iTotalSneakers = _repo.Sneaker.FindByCondition(b => b.BrandId == iBrandId)
                    .Count();
                sneakers = _repo.Sneaker.GetWithOptions(new QueryOptions<Sneaker>
                {
                    OrderBy = orderBy,
                    OrderByDirection = orderByDirection,
                    Where = p => p.BrandId == iBrandId,
                    PageNumber = productPage,
                    PageSize = iPageSize
                });
            }

            var model = new SneakerListViewModel
            {
                Sneakers = sneakers,
                SelectedBrand = id,
                PagingInfo = new PagingInfoViewModel
                {
                    CurrentPage=productPage,
                    ItemsPerPage=iPageSize,
                    TotalItems = iTotalSneakers
                }
            };
            return View(model);
        }

        public IActionResult Details(int id)
        {
            Sneaker sneaker = _repo.Sneaker.GetById(id);
            if (sneaker is not null)
            {
                //use ViewBag to pass data to view
                ViewBag.ImageFileName = sneaker.Code + "_m.png";

                //bind sneaker to view
                return View(sneaker);
            }
            else
            {
                return RedirectToAction("List");
            }
        }
    }
}
