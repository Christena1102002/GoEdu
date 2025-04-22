using GoEdu.Models;
using GoEdu.ViewModel;

namespace GoEdu.Repositories
{
    public interface ILectureRepository : ICRUD<Lecture>
    {
        
        public List<Lecture> GetAllCourseLectures(int CourseID);
        public List<VMLectureSchedule> GetStudentLectureSchedual(int StudentID);
        public VMLectureDetails GetLectureVMByID(int id, int StudentID);
        public VMLectureWithInstructorCourses  GetLectureWithCourseList(int LectureId, int InstructorID);

        public List<VMLectureSchedule> GetTodayLectureByStudentId(int StudentID);
        public List<VMLectureSchedule> GetLateLectures(int StudentID);

        #region Mark Section

        public List<LectureWithInstructorVM> GetLectureCourses(int CrsID);
        public int LectureCount(int CrsID);
        public void SaveEdit(AddOrEditLectureVM LctFromReq);

        public LectureDetailsVM LctDetails(int LctID);
        #endregion
    }
}
