using Evara.Data;
using Evara.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evara.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Index(int id)
        {
            Category category = _db.Categories.FirstOrDefault(item => item.CategoryID == id);
            ViewBag.Category = category;
            IEnumerable<Product> product = _db.Products.Include("Category").Where(sp => sp.CategoryID == id);
            return View(product);
        }
    }
}
