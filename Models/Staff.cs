using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDevelopment0805.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Address { get; set; }

        [ForeignKey("User")]
        public string StaffId { get; set; }
        public ApplicationUser User { get; set; }
    }
}