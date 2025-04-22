using GoEdu.Data;
using GoEdu.Models;

namespace GoEdu.Repositories
{
    public class EnrollRepository : IEnrollRepository
    {
        private GoEduContext context;

        public EnrollRepository(GoEduContext context)
        {
            this.context = context;
        }

        public void Delete(Enroll Entity)
        {
            throw new NotImplementedException();
        }

        public List<Enroll> GetAll()
        {
            throw new NotImplementedException();
        }

        public Enroll GetByID(int id)
        {
            throw new NotImplementedException();
        }
        public Enroll GetBy3IDs(int CourseID, int InstructorID,int StudentID)
        {
            return context.Enrolls.FirstOrDefault(e=>e.InstructorID==InstructorID 
                && e.CourseID==CourseID
                && e.StudentID==StudentID
            );
        }
        ////
        //public Lecture GetByStudentID(int id)
        //{
        //    return context.lectures.FirstOrDefault(d => d.ID == id);
        //}


        public void Insert(Enroll Entity)
        {
            context.Enrolls.Add(Entity);
        }

        public void SaveData()
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Enroll Entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
