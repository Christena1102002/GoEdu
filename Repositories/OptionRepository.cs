using GoEdu.Data;
using GoEdu.Models;

namespace GoEdu.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private GoEduContext context;

        public OptionRepository(GoEduContext context)
        {
            this.context = context;
        }

        //public List<Option> GetOptionsByQuestion(int questionId)
        //{
        //    return context.Options.Where(opt => opt.QuestionId == questionId).ToList();
        //}



        public void Delete(Option Entity)
        {
            context.Options.Remove(Entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Option> GetAll()
        {
            return context.Options.ToList();
        }

        public Option GetByID(int id)
        {
            return context.Options.FirstOrDefault(opt => opt.Id == id);
        }

        public void Insert(Option Entity)
        {
            context.Options.Add(Entity);
        }

        public void SaveData()
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Option Entity)
        {
            context.Options.Update(Entity);
        }
    }
}
