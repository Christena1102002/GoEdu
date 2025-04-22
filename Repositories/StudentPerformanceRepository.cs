using GoEdu.Models;
using GoEdu.Data;
using GoEdu.ViewModel;
namespace GoEdu.Repositories
{
    public class StudentPerformanceRepository : IStudentPerformanceRepository
    {
        private readonly GoEduContext context;

        public StudentPerformanceRepository(GoEduContext context)
        {
            this.context = context;
        }
        public StudentPerformance GetBy2IDs(int studentID,int LectureID)
        {
            return context.StudentPerformances.FirstOrDefault(s=>s.StudentId==studentID && s.LectureId==LectureID);
        }

        public List<VMStudentPerformance> GetStudentPerformanceByStudentId(int studentID)
        {
            return context.StudentPerformances.Where(p => p.StudentId == studentID).Select(p => new VMStudentPerformance
            {
                StudentId = studentID,
                LectureID = p.LectureId,
                LectureTitle=p.Lecture.Title,
                StudentName = p.Student.Name,
                CourseName = p.Lecture.Course.Name,
                LectureTime = p.Lecture.LectureTime,
                Grade = p.Grade
            }).OrderByDescending(l => l.LectureTime).ToList();
        }

        public List<VMStudentPerformance> GetStudentPerformanceByLectureId(int LectureId)
        {
            return context.StudentPerformances.Where(p => p.Lecture.ID == LectureId).Select(p => new VMStudentPerformance
            {
                StudentId = p.StudentId,
                LectureID = p.LectureId,
                LectureTitle = p.Lecture.Title,
                StudentName = p.Student.Name,
                CourseName = p.Lecture.Course.Name,
                LectureTime = p.Lecture.LectureTime,
                Grade = p.Grade
            }).OrderByDescending(l => l.LectureTime).ToList();
        }

        public List<VMStudentPerformance> TopTenByInstructorId(int instructorId)
        {
            return context.StudentPerformances.Where(p => p.Lecture.Course.InstructorID == instructorId).Select(p => new VMStudentPerformance
            {
                StudentId = p.StudentId,
                LectureID = p.LectureId,
                LectureTitle = p.Lecture.Title,
                StudentName = p.Student.Name,
                CourseName = p.Lecture.Course.Name,
                LectureTime = p.Lecture.LectureTime,
                Grade = p.Grade
            }).OrderByDescending(l => l.LectureTime).OrderByDescending(g => g.Grade).Take(10).ToList();
        }
        public void Delete(StudentPerformance Entity)
        {
            throw new NotImplementedException();
        }

        public List<StudentPerformance> GetAll()
        {
            throw new NotImplementedException();
        }

        public StudentPerformance GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(StudentPerformance Entity)
        {
            context.StudentPerformances.Add(Entity);
        }

        public void SaveData()
        {
            throw new NotImplementedException();
        }

        public void Update(int id, StudentPerformance Entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
