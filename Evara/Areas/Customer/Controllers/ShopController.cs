using Evara.Data;
using Evara.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evara.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ShopController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            int pageSize = 6;
            IEnumerable<Product> products = _db.Products.Include(sp => sp.Category);
            ViewBag.TotalPages = products.Count() / pageSize + 1;
            return View(products);
        }

        [HttpPost]
        public IActionResult GetProduct()
        {
            IEnumerable<Product> products = _db.Products.Include(sp => sp.Category);
            return Json(new { data = products });
        }

        public IActionResult Detail(int id)
        {
            IEnumerable<String> sizes = _db.Products.Where(sp => sp.ProductID == id).Select(sp => sp.Size);
            ViewBag.Size = sizes;
            Product product = _db.Products.Include(sp => sp.Brand).Include(sp => sp.Category).FirstOrDefault(sp => sp.ProductID == id);
            IEnumerable<Product> relatedProduct = _db.Products.Where(sp => sp.CategoryID == product.CategoryID && sp.ProductID != product.ProductID).ToList();
            if(relatedProduct.Count() == 0)
            {
                relatedProduct = _db.Products.Where(sp => sp.BrandID == product.BrandID && sp.ProductID != product.ProductID).ToList();
            }
            ViewBag.RelatedProduct = relatedProduct;
            return View(product);
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
