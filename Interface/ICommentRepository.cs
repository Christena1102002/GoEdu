using GoEdu.Models;
using GoEdu.ViewModel;

namespace GoEdu.Repositories
{
    public interface ICommentRepository : ICRUD<Comment>
    {
        public List<VMComment> GetCommentsByLectureId(int lectureId);
        
    }
}
