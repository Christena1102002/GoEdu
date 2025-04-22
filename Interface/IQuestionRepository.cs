using GoEdu.Models;
using GoEdu.ViewModel;

namespace GoEdu.Repositories
{
    public interface IQuestionRepository:ICRUD<Question>
    {
        public VMQuestionWithQuestions GetAndAddQustionListByLectureId(int LectureId);

        public Question GetQuestionByContent(string content, int LectureId);
        public List<VMQuestionAnswer> GetQuestionsByLectureID(int LectureID);
    }
}
