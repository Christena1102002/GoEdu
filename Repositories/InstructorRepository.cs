using GoEdu.Data;
using GoEdu.Models;

namespace GoEdu.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {

        private GoEduContext context;
        

        public InstructorRepository(GoEduContext context)
        {
            this.context = context;
            
        }
        public Instructor GetUserByFK(string id)
        {
            return context.Instructors.FirstOrDefault(i => i.ApplicationUserId==id);
        }
        public Instructor GetByID(int id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Instructor> GetAll()
        {
            throw new NotImplementedException();
        }


        public void Insert(Instructor Entity)
        {
            context.Instructors.Add(Entity);
        }

        public void SaveData()
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Instructor Entity)
        {
            throw new NotImplementedException();
        }
    }
}
