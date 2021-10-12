using AppDevelopment0805.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AppDevelopment0805.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;
        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Categories
        public ActionResult Index(string searchCategory)
        {
            var categories = _context.Categories.ToList();
            if (!String.IsNullOrEmpty(searchCategory))
            {
                categories = categories.FindAll(p => p.Name.Contains(searchCategory));
            }
            return View(categories);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_context.Categories.Any(p => p.Name.Contains(model.Name)))
            {
                ModelState.AddModelError("Name", "Category Name Exists.");
                return View(model);
            }
            var newCategory = new Category
            {
                Name = model.Name,
                Description = model.Description
            };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(p => p.Id == id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }
            _context.Categories.Remove(categoryInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(p => p.Id == id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }
            return View(categoryInDb);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var categoryInDb = _context.Categories.SingleOrDefault(p => p.Id == category.Id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }
            categoryInDb.Name = category.Name;
            categoryInDb.Description = category.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(p => p.Id == id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }
            return View(categoryInDb);

        }
    }
}

