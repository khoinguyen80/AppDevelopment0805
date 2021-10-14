using AppDevelopment0805.Models;
using AppDevelopment0805.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AppDevelopment0805.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext _context;

        private UserManager<ApplicationUser> _userManager;
        public CoursesController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(
       new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }
        // GET: Courses

        [HttpGet]
        public ActionResult Index()
        {


            //var userId = User.Identity.GetUserId();

            var coursesInDb = _context.Courses
                .Include(t => t.Category)
                .ToList();
            //.Where(t => t.UserId.Equals(userId))

            return View(coursesInDb);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CourseCategoriesViewModel()
            {
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                var viewModels = new CourseCategoriesViewModel()
                {
                    Course = course,
                    Categories = _context.Categories.ToList()
                };

                return View(viewModels);
            }

            //var userId = User.Identity.GetUserId();
            var newCourse = new Course()
            {
                Description = course.Description,
                CategoryId = course.CategoryId,
                Name = course.Name,

            };

            _context.Courses.Add(newCourse);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            //var userId = User.Identity.GetUserId();

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var courseInDb = _context.Courses
                //.Where(t => t.UserId.Equals(userId))
                .SingleOrDefault(t => t.Id == id);

            if (courseInDb == null) return HttpNotFound();

            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}