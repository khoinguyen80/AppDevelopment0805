using AppDevelopment0805.Models;
using System.Collections.Generic;

namespace AppDevelopment0805.ViewModels
{
    public class TrainersCourseViewModel
    {
        public int CourseId { get; set; }
        public List<Course> Courses { get; set; }
        public int TrainerId { get; set; }
        public List<Trainer> Trainers { get; set; }
    }
}