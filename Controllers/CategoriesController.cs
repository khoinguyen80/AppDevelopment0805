using AppDevelopment0805.Models;
using AppDevelopment0805.Roles;
using AppDevelopment0805.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace AppDevelopment0805.Controllers
{
    [Authorize(Roles = Role.Staff)]
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


        [HttpGet]
        public ActionResult GetTrainees(string SearchCourse)
        {
            var courses = _context.Courses
                .Include(t => t.Category)
                .ToList();
            var trainee = _context.TraineesCourses.ToList();

            List<CourseTraineesViewModel> viewModel = _context.TraineesCourses
                .GroupBy(i => i.Course)
                .Select(rs => new CourseTraineesViewModel
                {
                    Course = rs.Key,
                    Trainees = rs.Select(u => u.Trainee).ToList()
                })
                .ToList();
            if (!string.IsNullOrEmpty(SearchCourse))
            {
                viewModel = viewModel
                    .Where(t => t.Course.Name.ToLower().Contains(SearchCourse.ToLower())).
                    ToList();
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult AddTrainee()
        {
            var viewModel = new TraineesCourseViewModel
            {
                Courses = _context.Courses.ToList(),
                Trainees = _context.Trainees.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AddTrainee(TraineesCourseViewModel viewModel)
        {
            var model = new TraineesCourse
            {
                CourseId = viewModel.CourseId,
                TraineeId = viewModel.TraineeId
            };
            List<TraineesCourse> traineesCourses = _context.TraineesCourses.ToList();
            bool alreadyExist = traineesCourses.Any(item => item.CourseId == model.CourseId && item.TraineeId == model.TraineeId);
            if (alreadyExist == true)
            {
                ModelState.AddModelError("", "Trainee assignned this Course");
                return RedirectToAction("GetTrainees", "Courses");
            }
            _context.TraineesCourses.Add(model);
            _context.SaveChanges();

            return RedirectToAction("GetTrainees", "Courses");
        }

        [HttpGet]
        public ActionResult RemoveTrainee()
        {
            var trainees = _context.TraineesCourses.Select(t => t.Trainee)
                .Distinct()
                .ToList();
            var courses = _context.TraineesCourses.Select(t => t.Course)
                .Distinct()
                .ToList();

            var viewModel = new TraineesCourseViewModel
            {
                Courses = courses,
                Trainees = trainees
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult RemoveTrainee(TraineesCourseViewModel viewModel)
        {
            var courseTrainee = _context.TraineesCourses
                .SingleOrDefault(t => t.CourseId == viewModel.CourseId && t.TraineeId == viewModel.TraineeId);
            if (courseTrainee == null)
            {
                ModelState.AddModelError("", "Trainee is not assignned in this Course");
                return RedirectToAction("GetTrainees", "Courses");
            }

            _context.TraineesCourses.Remove(courseTrainee);
            _context.SaveChanges();

            return RedirectToAction("GetTrainees", "Courses");
        }
    }
}

