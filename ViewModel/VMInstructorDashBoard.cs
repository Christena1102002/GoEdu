using System.ComponentModel.DataAnnotations;

namespace GoEdu.ViewModel
{
    public class VMInstructorDashBoard
    {
        public int InstructorId { get; set; }
        public int TotalStudents { get; set; }
        public int TotalViews {  get; set; }
        public int TotalCourses {  get; set; }
        public List<VMStudentPerformance> TopTenPerformances { get; set; }
    }
}
