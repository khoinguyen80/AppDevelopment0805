using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppDevelopment0805.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        [DisplayName("Category Name")]
        [Index("Name_Index", IsUnique = true)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

    }
}