using GoEdu.Data;
using GoEdu.Models;
using GoEdu.ViewModel;

namespace GoEdu.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private GoEduContext context;

        public QuestionRepository(GoEduContext context)
        {
            this.context = context;
        }

        public VMQuestionWithQuestions GetAndAddQustionListByLectureId(int LectureId)
        {
            VMQuestionWithQuestions qustionList = new();

            qustionList.Questions = context.Questions.Where(q => q.LectureId == LectureId).Select(q => new VMQuestionOptionList
            {
                QuestionId = q.Id,
                Content = q.Content,
                ModelAnswer = q.ModelAnswer,
                Options = q.Options
            }
            ).ToList();

            qustionList.LectureId = LectureId;

            return qustionList;
        }
        //show question for student by lecture
        public List<VMQuestionAnswer> GetQuestionsByLectureID(int LectureID) {
            return context.Questions.Where(q=>q.LectureId==LectureID)
                .Select(q=>new VMQuestionAnswer {
                    Content = q.Content,
                    LectureId = q.LectureId,
                    ModelAnswer=q.ModelAnswer,
                    QuestionId = q.Id,
                    options=q.Options
                })
                .ToList();
        }
        public Question GetQuestionByContent(string content, int LectureId)
        {
            return context.Questions.FirstOrDefault(q => q.Content == content && q.LectureId == LectureId);
        }


        //public List<VMQuestionOptionList> GetQustionByLecture(int LectureId)
        //{
        //    List<VMQuestionOptionList> qustionList = new();

        //    qustionList = context.Questions.Where(q => q.LectureId == LectureId).Select(q => new VMQuestionOptionList
        //    {
        //        QuestionId = q.Id,
        //        Content = q.Content,
        //        ModelAnswer = q.ModelAnswer,
        //        Options = q.Options,
        //        LectureId = q.LectureId
        //    }
        //    ).ToList();

        //    return qustionList;
        //}
        //public List<Question> GetQustionByLecture(int LectureId)
        //{
        //    return context.Questions.Where(q=>q.LectureId == LectureId).ToList();
        //}

        public void Delete(Question Entity)
        {
            context.Questions.Remove(Entity);
        }

        public List<Question> GetAll()
        {
            throw new NotImplementedException();
        }

        public Question GetByID(int id)
        {
            return context.Questions.FirstOrDefault(q => q.Id == id);
        }


        public void Insert(Question Entity)
        {
            context.Questions.Add(Entity);
        }

        public void SaveData()
        {
            context.SaveChanges();
        }

        public void Update(int id, Question ques)
        {
            //Question qust = context.Questions.FirstOrDefault(x=>x.Id==id);

            context.Questions.Update(ques);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}