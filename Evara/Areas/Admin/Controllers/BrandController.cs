using Evara.Data;
using Evara.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Evara.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BrandController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            IEnumerable<Brand> brands = _db.Brands.ToList();
            return View(brands);
        }
        [HttpGet]
        public IActionResult Upsert(int id)
        {
            Brand brand = new Brand();
            if (id == 0)
            {
                return View(brand);
            }
            brand = _db.Brands.FirstOrDefault(i => i.BrandID == id);
            return View(brand);
        }

        [HttpPost]
        public IActionResult Upsert(Brand brand)
        {
            if (ModelState.IsValid)
            {
                if (brand.BrandID == 0)
                {
                    _db.Brands.Add(brand);
                }
                else
                {
                    _db.Brands.Update(brand);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
