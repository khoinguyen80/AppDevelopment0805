using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDevelopment0805.Models
{
    public class Course
    {

        [Required]
        [Display(Name = "Course Id")]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }    // Linking Object to Category model

        [Required]

        [Display(Name = "Course Name")]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}