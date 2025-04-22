using GoEdu.Models;

namespace GoEdu.Repositories
{
    public interface IAttendRepository : ICRUD<Attend>
    {
        public Attend GetBy2Ids(int StudentId, int LectureId);
        public int LecturesTotalViewsByInstructorId(int instructorId);

    }
}
