using GoEdu.Models;

namespace GoEdu.ViewModel
{
    public class StudentsCoursesVM
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public StatusVM status { get; set; }
        public int CourseId { get; set; }
        public int statusValue { get; set; }

        //public int InstructorId { get; set; }

        public StudentsCoursesVM()
        {
            status = new();
        }

    }


    public enum StatusVM
    {
        All,
        Active,
        Completed
    }
}
