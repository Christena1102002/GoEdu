using System.Collections.Generic;
using GoEdu.Data;
using GoEdu.Models;

namespace GoEdu.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly GoEduContext ctx;

        public StudentRepository(GoEduContext ctx)
        {
            this.ctx = ctx;
        }
        public Student GetUserByFK(string id)
        {
            return ctx.Students.FirstOrDefault(s => s.ApplicationUserId == id);
        }

        public List<Student> GetStudentsByInstructor(int instructorId)
        {
            List<Student> std = ctx.Enrolls.Where(r=>r.InstructorID == instructorId).Select(r=>r.Student).ToList();

            return std;
        }

        public List<Student> GetStudentsByCourse(int CoureId)
        {
            List<Student> std = ctx.Enrolls.Where(r => r.CourseID == CoureId).Select(r => r.Student).ToList();

            return std;
        }

        public List<Student> GetAll()
        {
            return ctx.Students.ToList();
        }

        public Student GetByID(int id)
        {
            return ctx.Students.FirstOrDefault(s=>s.ID ==  id);
        }

        public void Insert(Student Entity)
        {
            ctx.Students.Add(Entity);
        }

        public void Update(int id, Student Entity)
        {
            //Student std = ctx.Students.FirstOrDefault(Entity);
            ctx.Students.Update(Entity);
        }

        public void SaveData()
        {
            ctx.SaveChanges();
        }

        public void Delete(Student Entity)
        {
            ctx.Students.Remove(Entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
