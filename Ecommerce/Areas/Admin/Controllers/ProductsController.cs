using Microsoft.AspNetCore.Mvc;
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using Humanizer;
namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        //Done
        public IActionResult Index()
        {
            //join
            var products = _context.Products.Include(p=>p.Category).ToList();
            var productsVm = new List<ProductsViewModel>();


            foreach (var product in products)
            {
                var vmItem = new ProductsViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Rate = product.Rate,
                    Quantity = product.Quantity,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/uploads/products/{product.Image}",
                    CategoryName = product.Category.Name
                };
                productsVm.Add(vmItem);
        }

            return View(productsVm);
        }

        //To create product page
        public IActionResult Create() {
          ViewBag.categories= _context.Categories.ToList();
          return View(new Product());
        }
        //Done

        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product request, IFormFile file) {
            ModelState.Remove("Rate");
            ModelState.Remove("file");
            ViewBag.categories = _context.Categories.ToList();


            //1. modelstate validation (server side validation) =>(all inputs without file(image) input )
            if (!ModelState.IsValid) {
                return View("Create", request);
            }

            //2. file validation (server side validation) (case :file is required)

            //a. file is empty case
            if (file == null || file.Length < 0)
            {
                ModelState.AddModelError("Image", "Please Upload Product Image!!");
                return View("Create", request);
            }
            //b. allowed extentions test
            var allowedExtentions = new[] { ".jpg", ".png", ".jpeg" };
            var fileExtention = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtentions.Contains(fileExtention))
            {
                ModelState.AddModelError("Image", "Only jpg, png, jpeg files are allowed!");
                return View("Create", request);
            }
            //c. file size test => if length >2 Mega

            if (file.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Image size must be less than 2 MB !");
                return View("Create", request);
            }



            //if everything is ok=> add product 
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploads\products", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                request.Image = fileName;

                _context.Products.Add(request);
                _context.SaveChanges();

            //for sweet alert in admin layout
            TempData["Success"] = "New Product Added Successfully!";

            return RedirectToAction(nameof(Index));
         
         
        }
   
        //to edit page
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            ViewBag.categories = _context.Categories.ToList();
            return View(product);
        }
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProduct(int id ,Product request, IFormFile? file)
        {
            var product = _context.Products.Find(id);
            // الاحتفاظ بقيمة الصورة لان ممكن يظهر ايرور بالفالديشن ويرجع قيمة ريكويست لفورم التعديل وبالتالي بدون هذه الخطوة لا يتم عرض الصورة القديمة اذا كان في خطأ باختيار الصورة 
            request.Image = product.Image;
            ViewBag.categories = _context.Categories.ToList();


            //1. modelstate validation (server side validation) 
            ModelState.Remove("file");
            ModelState.Remove("Rate");
            if (!ModelState.IsValid)
            {
                return View("Edit", request);
            }
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.CategoryId = request.CategoryId; 

            //تعديل الصورة اختياري 
            //2. if we edit the image: 1. chick validation 2. update it in database 
            if (file != null && file.Length > 0)
            {
                //1. server side validation 
                //a. allowed extentions test
                var allowedExtentions = new[] { ".jpg", ".png", ".jpeg" };
                var fileExtention = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtentions.Contains(fileExtention))
                {
                    ModelState.AddModelError("Image", "Only jpg, png, jpeg files are allowed!");
                    return View("Edit", request);
                }
                //b. file size test => if length >2 Mega

                if (file.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("Image", "Image size must be less than 2 MB !");
                    return View("Edit", request);
                }

                //if everything is ok=>
                //1.delete old image from folder 
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploads\products", product.Image);
                System.IO.File.Delete(oldFilePath);
                //2.add & update category image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploads\products", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                product.Image = fileName;
            }

            _context.SaveChanges();
            //for sweet alert in admin layout
            TempData["Success"] = "Updated Successfully!";

            return RedirectToAction(nameof(Index));
        }
        

        
        //Done
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            //step1:
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploads\products", product.Image);
            System.IO.File.Delete(filePath);
            //step2:
            _context.Products.Remove(product);
            _context.SaveChanges();

            TempData["Success"] = "Deleted Successfully!";
            return RedirectToAction(nameof(Index));

        }
  

    }
}
