using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagementMVC.Models
{
	public class Student
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(1, 120)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public string Course { get; set; }

        [Range(1, 10, ErrorMessage = "Semester must be between 1 and 10")]
        public int Semester { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}