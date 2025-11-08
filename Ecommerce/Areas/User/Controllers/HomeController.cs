
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        ApplicationDbContext _context= new ApplicationDbContext();

        //Home Page
        public IActionResult Index()
        {
            //ViewModel (categories)
            var categories = _context.Categories.ToList();
            var categoriesVm= new List<CategoriesViewModel>();
            foreach (var category in categories)
            {
                var categoryVm = new CategoriesViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/uploads/categories/{category.Image}"
                };
                categoriesVm.Add(categoryVm);
            }
            ViewBag.categories = categoriesVm;

            //ViewModel( products)
            var first4Products=_context.Products.Take(4).Include(p => p.Category).ToList();
            var first4ProductsVm= new List<ProductsViewModel>();
            foreach (var product in first4Products)
            {
                var productVm = new ProductsViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Rate = product.Rate,
                    Quantity = product.Quantity,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/uploads/products/{product.Image}",
                    CategoryName = product.Category.Name,
                };
                first4ProductsVm.Add(productVm);
            }
            ViewBag.first4Products=first4ProductsVm;


            ViewBag.ProductCount =_context.Products.Count();
            ViewBag.CategoryCount=_context.Categories.Count(); 
            return View();
        }

        public IActionResult AllProducts() {
            var products = _context.Products.Include(p => p.Category).ToList();
            var productsVm = new List<ProductsViewModel>();
            foreach (var product in products)
            {
                var productVm = new ProductsViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Rate = product.Rate,
                    Quantity = product.Quantity,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/uploads/products/{product.Image}",
                    CategoryName = product.Category.Name,
                };
                productsVm.Add(productVm);
            }
            return View(productsVm);
        }

        public IActionResult ProductDetails(int id) {
            var product = _context.Products.Include(p=>p.Category).FirstOrDefault(p=>p.Id==id);
            ViewBag.productVm = new ProductsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Rate = product.Rate,
                Quantity = product.Quantity,
                ImageUrl = $"{Request.Scheme}://{Request.Host}/uploads/products/{product.Image}",
                CategoryName = product.Category.Name,
            };

            return View();
        }

    }
}
