using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TrainingApplication.Models;

namespace AppDevelopment0805.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Trainee> Trainees { get; set; }

        public DbSet<TraineesCourse> TraineesCourses { get; set; }

        public DbSet<TrainersCourse> TrainersCourses { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}