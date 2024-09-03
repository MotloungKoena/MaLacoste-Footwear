using MaLacoste_Footwear.Data;
using MaLacoste_Footwear.Models;
using MaLacoste_Footwear.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaLacoste_Footwear.Controllers
{
   [Authorize(Roles = "Admin")]
    public class AdminSneakerController : Controller
    {
        private IEnumerable<Brand> _brands;
        private readonly IRepositoryWrapper _repo;

        public AdminSneakerController(IRepositoryWrapper repo)
        {
            _repo = repo;
            _brands = _repo.Brand.FindAll()
                .OrderBy(b => b.BrandName);
        }
        public IActionResult List(string id = "all")
        {
            IEnumerable<Sneaker> sneakers;
            if (id == "all")
            {
                sneakers = _repo.Sneaker.FindAll()
                     .OrderBy(p => p.Name);
            }
            else
            {
                sneakers =_repo.Sneaker.GetAllSneakersWithBrandDetails()
                    .Where(s => s.Brand.BrandName.ToLower() == id.ToLower())
                    .OrderBy(b => b.Name);
            }

            var model = new SneakerListViewModel
            {
                Sneakers = sneakers,
                SelectedBrand = id
            };

            //bind sneakers to view
            return View(model);
        }

        public IActionResult Add()
        {
            //create new Sneaker object
            Sneaker sneaker = new(); //create sneaker object

            sneaker.Brand = _repo.Brand.GetById(1); // add Brand object - prevents validation problem

            ViewBag.Action = "Add";
            ViewBag.Brands = _brands;

            return View("AddUpdate", sneaker);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Sneaker sneaker = _repo.Sneaker.GetById(id);

            ViewBag.Action = "Update";
            ViewBag.Brands = _brands;

            return View("AddUpdate", sneaker);
        }
        [HttpPost]
        public IActionResult Update(Sneaker sneaker)
        {
            if (ModelState.IsValid)
            {
                if (sneaker.SneakerId == 0) 
                {
                    _repo.Sneaker.Create(sneaker);
                    TempData["Message"] = $"{sneaker.Name} has been added";
                }
                else 
                {
                    _repo.Sneaker.Update(sneaker);
                    TempData["Message"] = $"{sneaker.Name} has been updated";
                }
                _repo.Save();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Action = "Save";
                ViewBag.Brands = _brands;
                return View("AddUpdate", sneaker);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Sneaker sneaker = _repo.Sneaker.GetById(id);
            TempData["Message"] = $"{sneaker.Name} has been deleted";
            return View(sneaker);
        }
        [HttpPost]
        public IActionResult Delete(Sneaker sneaker)
        {
            _repo.Sneaker.Delete(sneaker);
            _repo.Save();
            return RedirectToAction("List");
        }
    }
}
