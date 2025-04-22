using GoEdu.Models;

namespace GoEdu.Repositories
{
    public interface IEnrollRepository : ICRUD<Enroll>
    {
        public Enroll GetBy3IDs(int CourseID, int InstructorID, int StudentID);
    }
}
