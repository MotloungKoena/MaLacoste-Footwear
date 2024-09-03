using MaLacoste_Footwear.Data;
using MaLacoste_Footwear.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaLacoste_Footwear.Controllers
{
    public class ReviewController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ReviewController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Genders = await _appDbContext.Genders.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Add(review);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Confirmation");
            }
            ViewBag.Genders = await _appDbContext.Genders.ToListAsync();
            return View(review);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reviews = await _appDbContext.Reviews.Include(r => r.Gender).ToListAsync();
            return View(reviews);
        }

        public IActionResult Confirmation() { return View(); }
    }
}
