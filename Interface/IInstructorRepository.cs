using GoEdu.Models;

namespace GoEdu.Repositories
{
    public interface IInstructorRepository : ICRUD<Instructor>
    {
        public Instructor GetUserByFK(string id);
        
    }
}
