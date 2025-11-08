using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        ApplicationDbContext _context=new ApplicationDbContext();
        //show all categories
        public IActionResult Index()
        {
            //ViewModel (categories)
            var categories = _context.Categories.ToList();
            var categoriesVm = new List<CategoriesViewModel>();
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
            return View();
        }
        //to create page
        public IActionResult Create()
        {
           return View(new Category());
        }

        // add operation
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(Category request,IFormFile file) {

            //1. modelstate validation (server side validation) =>(Name)
            ModelState.Remove("file");
            if (!ModelState.IsValid) {
                return View("Create", request);
            }
            //2. file validation (server side validation) (case :file is required)

            //a. file is empty case
            if (file == null || file.Length < 0) {
                ModelState.AddModelError("Image", "Please Upload Category Image!!");
                return View("Create", request);
            }
            //b. allowed extentions test
            var allowedExtentions = new[] {".jpg",".png",".jpeg"};
            var fileExtention = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtentions.Contains(fileExtention))
            {
                ModelState.AddModelError("Image", "Only jpg, png, jpeg files are allowed!");
                return View("Create", request);
            }
            //c. file size test => if length >2 Mega

            if (file.Length > 2 * 1024 * 1024) {
                ModelState.AddModelError("Image", "Image size must be less than 2 MB !");
                return View("Create", request);
            }

            //if everything is ok=> add category 
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploads\categories", fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }
            request.Image = fileName;

            _context.Categories.Add(request);
            _context.SaveChanges();

            //for sweet alert in admin layout
            TempData["Success"] = "New Category Added Successfully!";

            return RedirectToAction(nameof(Index));

        }

        //to edit page
        public IActionResult Edit(int id)
        {
            // يجب ارسالها كموديل لكي يستطيع التعرف لوحده على الvalue 
            //باستخدام الtag helper
            var category = _context.Categories.Find(id);
            return View(category);
        }

        //update operation
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(int id, Category request, IFormFile? file)
        {
            var category= _context.Categories.Find(id);
            // الاحتفاظ بقيمة الصورة لان ممكن يظهر ايرور بالفالديشن ويرجع قيمة ريكويست لفورم التعديل وبالتالي بدون هذه الخطوة لا يتم عرض الصورة القديمة اذا كان في خطأ باختيار الصورة 
            request.Image= category.Image;


            //1. modelstate validation (server side validation) =>(Name)
            ModelState.Remove("file");
            if (!ModelState.IsValid)
            {
                return View("Edit", request);
            }
            category.Name= request.Name;

            //تعديل الصورة اختياري 
            //2. if we edit the image: 1. chick validation 2. update it in database 
            if (file != null && file.Length>0 ) 
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
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploads\categories", category.Image);
                System.IO.File.Delete(oldFilePath);
                //2.add & update category image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploads\categories", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                category.Image = fileName;
            }

            _context.SaveChanges();
            //for sweet alert in admin layout
            TempData["Success"] = "Updated Successfully!";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id) {
            var category = _context.Categories.Find(id);
            //step1:
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploads\categories", category.Image);
            System.IO.File.Delete(filePath);
           //step2:
            _context.Categories.Remove(category);  
            _context.SaveChanges();

           //for sweet alert in admin layout
            TempData["Success"]= "Deleted Successfully!";

            return RedirectToAction(nameof(Index));
        }
    }
}
