using AppDevelopment0805.Models;
using AppDevelopment0805.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TrainingApplication.Models;
using TrainingApplication.ViewModels;

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
        public ActionResult Edit(int id)
        {

            //var userId = User.Identity.GetUserId();

            var courseInDb = _context.Courses
                //.Where(t => t.UserId.Equals(userId))
                .SingleOrDefault(t => t.Id == id);

            if (courseInDb == null) return HttpNotFound();

            var viewModels = new CourseCategoriesViewModel
            {
                Course = courseInDb,
                Categories = _context.Categories.ToList()
            };

            return View(viewModels);
        }


        [HttpPost]
        public ActionResult Edit(Course course)
        {
            //var userId = User.Identity.GetUserId();
            var courseInDb = _context.Courses
                //.Where(t => t.UserId.Equals(userId))
                .SingleOrDefault(t => t.Id == course.Id);

            if (!ModelState.IsValid)
            {
                var viewModels = new CourseCategoriesViewModel
                {
                    Course = course,
                    Categories = _context.Categories.ToList()
                };
                return View(viewModels);
            }

            if (courseInDb == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            courseInDb.CategoryId = course.CategoryId;
            courseInDb.Name = course.Name;
            courseInDb.Description = course.Description;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            //var userId = User.Identity.GetUserId();


            var courseInDb = _context.Courses
                //.Where(t => t.UserId.Equals(userId))
                .SingleOrDefault(t => t.Id == id);

            if (courseInDb == null) return HttpNotFound();

            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetTrainers(string SearchCourse)
        {
            var courses = _context.Courses
                .Include(t => t.Category)
                .ToList();
            var trainer = _context.TrainersCourses.ToList();

            List<CourseTrainersViewModel> viewModel = _context.TrainersCourses
                .GroupBy(i => i.Course)
                .Select(rs => new CourseTrainersViewModel
                {
                    Course = rs.Key,
                    Trainers = rs.Select(u => u.Trainer).ToList()
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
        public ActionResult AddTrainer()
        {
            var viewModel = new TrainersCourseViewModel
            {
                Courses = _context.Courses.ToList(),
                Trainers = _context.Trainers.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AddTrainer(TrainersCourseViewModel viewModel)
        {
            var model = new TrainersCourse
            {
                CourseId = viewModel.CourseId,
                TrainerId = viewModel.TrainerId
            };

            List<TrainersCourse> trainersCourses = _context.TrainersCourses.ToList();
            bool alreadyExist = trainersCourses.Any(item => item.CourseId == model.CourseId && item.TrainerId == model.TrainerId);
            if (alreadyExist == true)
            {
                ModelState.AddModelError("", "Trainer is already assignned this Course");
                return RedirectToAction("GetTrainers", "Courses");
            }
            _context.TrainersCourses.Add(model);
            _context.SaveChanges();

            return RedirectToAction("GetTrainers", "Courses");
        }

        [HttpGet]
        public ActionResult RemoveTrainer()
        {
            var trainers = _context.TrainersCourses.Select(t => t.Trainer)
                .Distinct()
                .ToList();
            var courses = _context.TrainersCourses.Select(t => t.Course)
                .Distinct()
                .ToList();

            var viewModel = new TrainersCourseViewModel
            {
                Courses = courses,
                Trainers = trainers
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult RemoveTrainer(TrainersCourseViewModel viewModel)
        {
            var courseTrainer = _context.TrainersCourses
                .SingleOrDefault(t => t.CourseId == viewModel.CourseId && t.TrainerId == viewModel.TrainerId);
            if (courseTrainer == null)
            {
                ModelState.AddModelError("", "Trainer is not assignned in this Course");
                return RedirectToAction("GetTrainers", "Courses");
            }

            _context.TrainersCourses.Remove(courseTrainer);
            _context.SaveChanges();

            return RedirectToAction("GetTrainers", "Courses");
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
                ModelState.AddModelError("", "Trainee is already assignned this Course");
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