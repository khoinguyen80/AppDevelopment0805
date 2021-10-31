using AppDevelopment0805.Models;
using System.Collections.Generic;

namespace AppDevelopment0805.ViewModels
{
    public class TraineesCourseViewModel
    {
        public int CourseId { get; set; }
        public List<Course> Courses { get; set; }
        public int TraineeId { get; set; }
        public List<Trainee> Trainees { get; set; }
    }
}