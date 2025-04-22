using System.ComponentModel.DataAnnotations;
using GoEdu.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoEdu.ViewModel
{
    public class CourseViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Course Name Must be less than 50 char")]
        [MinLength(2, ErrorMessage = "Course Name Must be More than 1 char")]
        public string Name { get; set; }


        [Range(50, 10000, ErrorMessage = "Invalid Price")]
        public double Price { get; set; }

        [Range(1, 1000, ErrorMessage = "Invalid Hours, must be positive")]
        public int Hours { get; set; }
        public int MaxViews { get; set; }


        public Level CourseLevel { get; set; }
        public Semester Semester { get; set; }
        public Stage StudentStage { get; set; }
        public Level StudentLevel { get; set; }
        public int InstructorID { get; set; }
        public bool isDeleted { get; set; } = false;


        public string InstructorName { get; set; }
        //public virtual Instructor? Instructor{ get; set; }
    }
}


