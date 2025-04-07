using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentManagementMVC.Models
{
	public class StudentDbContext:DbContext
	{
        public StudentDbContext() : base("StudentConnection")
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}