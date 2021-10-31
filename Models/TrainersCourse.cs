using AppDevelopment0805.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingApplication.Models
{
    public class TrainersCourse
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Trainer")]
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}