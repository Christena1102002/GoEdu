using GoEdu.Models;
using GoEdu.ViewModel;

namespace GoEdu.Repositories
{
    public interface IStudentPerformanceRepository:ICRUD<StudentPerformance>
    {
        public StudentPerformance GetBy2IDs(int studentID, int LectureID);
        public List<VMStudentPerformance> GetStudentPerformanceByStudentId(int studentID);
        public List<VMStudentPerformance> GetStudentPerformanceByLectureId(int InstructorId);
        public List<VMStudentPerformance> TopTenByInstructorId(int instructorId);
    }
}
