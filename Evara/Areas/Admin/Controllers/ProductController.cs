using Evara.Data;
using Evara.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Evara.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            IEnumerable<Product> products = _db.Products.Include("Brand").Include("Category").ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Upsert(int id)
        {
            Product product = new Product();
            IEnumerable<SelectListItem> brandList = _db.Brands.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.BrandID.ToString()
            });
            IEnumerable<SelectListItem> categoryList = _db.Categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CategoryID.ToString()
            });
            ViewBag.BrandList = brandList;
            ViewBag.CategoryList = categoryList;
            if(id == 0)
            {
                return View(product);
            }
            product = _db.Products.Include("Brand").Include("Category").FirstOrDefault(i => i.ProductID == id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Upsert(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    _db.Products.Add(product);
                }
                else
                {
                    _db.Products.Update(product);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
