using GoEdu.Data;
using GoEdu.Models;
using GoEdu.ViewModel;

namespace GoEdu.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly GoEduContext context;

        public CommentRepository(GoEduContext context)
        {
            this.context = context;
        }

        public List<VMComment> GetCommentsByLectureId(int lectureId)
        {
            return context.Comments.Where(c => c.LectureID == lectureId).Select(c => new VMComment
            {
                Content = c.Content,
                Date = c.Date,
                UserName = c.Student.Name,
                UserId = c.UserID,
                UserImageUrl = c.Student.ImageUrl,
            }).ToList();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Comment GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Comment Entity)
        {
            context.Comments.Add(Entity);
        }

        public void SaveData()
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Comment Entity)
        {
            throw new NotImplementedException();
        }
    }
}
