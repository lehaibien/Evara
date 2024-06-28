using Evara.Data;
using Evara.Models;
using Microsoft.AspNetCore.Mvc;

namespace Evara.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            IEnumerable<Category> categories = _db.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Upsert(int id)
        {
            Category category = new Category();
            if (id == 0)
            {
                return View(category);
            }
            category = _db.Categories.FirstOrDefault(i => i.CategoryID == id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Upsert(Category categories)
        {
            if (ModelState.IsValid)
            {
                if (categories.CategoryID == 0)
                {
                    _db.Categories.Add(categories);
                }
                else
                {
                    _db.Categories.Update(categories);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
