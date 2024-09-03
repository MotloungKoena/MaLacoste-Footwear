using MaLacoste_Footwear.Data;
using MaLacoste_Footwear.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaLacoste_Footwear.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBrandController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        public AdminBrandController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public IActionResult List()
        {
            var brands = _repo.Brand.FindAll().OrderBy(b => b.BrandName).ToList();
            return View(brands);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddUpdate", new Brand());
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Action = "Update";
            var brand = _repo.Brand.GetById(id);
            return View("AddUpdate", brand);
        }
        [HttpPost]
        public IActionResult Update(Brand brand)
        {
            if (ModelState.IsValid)
            {
                if (brand.BrandId is 0)
                {
                    _repo.Brand.Create(brand);
                    TempData["Message"] = $"{brand.BrandName} has been added";
                }
                else
                {
                    _repo.Brand.Update(brand);
                    TempData["Message"] = $"{brand.BrandName} has been updated";
                }
                _repo.Save();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Action = "Save";
                return View("AddUpdate");
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Brand brand = _repo.Brand.GetById(id);
            TempData["Message"] = $"{brand.BrandName} has been deleted";
            return View(brand);
        }
        [HttpPost]
        public IActionResult Delete(Brand brand)
        {
            _repo.Brand.Delete(brand);
            _repo.Save();
            return RedirectToAction("List");
        }
    }
}
