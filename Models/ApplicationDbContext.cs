﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AppDevelopment0805.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Category> Categories { get; set; } 

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}