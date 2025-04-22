using System.ComponentModel.DataAnnotations;
using GoEdu.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoEdu.ViewModel
{
    public class CourseWithInstructorVM
    {
        public int CrsID { get; set; }
        public string CrsName { get; set; }
        public string ImageUrl { get; set; }

        public double CrsPrice { get; set; }
        public int CrsHours { get; set; }
        public double CrsMinDegree { get; set; }
        public Semester CrsSemester { get; set; }
        public Stage CrsStage { get; set; }
        public Level CrsLevel { get; set; }
        public int InsID { get; set; }
        
        public int NumOfAllStudent { get; set; }
        public int NumOfStudent { get; set; }
        public int NumOfLecture { get; set; }
        public int NumOfCourses { get; set; }

        public List<Course> courses { get; set; }

    }

}
