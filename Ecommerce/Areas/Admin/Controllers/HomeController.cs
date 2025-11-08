using Microsoft.AspNetCore.Mvc;
using Ecommerce.Data;
namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        ApplicationDbContext _context= new ApplicationDbContext();
        public IActionResult Index()
        {
            ViewBag.categoriesCount = _context.Categories.Count();
            ViewBag.productsCount = _context.Products.Count();
            return View();
        }
    }
}
