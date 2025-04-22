using System.Linq;
using GoEdu.Data;
using GoEdu.Models;
using GoEdu.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoEdu.Repositories
{
    internal class LectureRepository:ILectureRepository
    {
        private GoEduContext context;
        private UnitOfWork unitOfWork;

        public LectureRepository(GoEduContext context, UnitOfWork unitOfWork)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
        }

        #region Mark Section
        #region Get Course Lectures
        public List<LectureWithInstructorVM> GetLectureCourses(int CrsID)
        {
            List<LectureWithInstructorVM> lectures = context.lectures.Where(l => l.CourseID == CrsID && l.isDeleted == false)
                    .Select(l => new LectureWithInstructorVM()
                    {
                        LctID = l.ID,
                        Title = l.Title,
                        LctTime = l.LectureTime,
                        Description = l.Description,
                        CrsID = l.CourseID
                    }).AsNoTracking().ToList();

            return lectures;
        }
         
        public void SaveEdit(AddOrEditLectureVM LctFromReq)
        {
            Lecture? LctFromDB = context.lectures.FirstOrDefault(l => l.ID == LctFromReq.LctID);
            LctFromDB.Title = LctFromReq.Title;
            LctFromDB.LectureTime = LctFromReq.LectureTime;
            LctFromDB.Description = LctFromReq.Description;
            LctFromDB.VideoURL = LctFromReq.VideoURL;


            SaveData();
        }
        
        public int LectureCount(int CrsID)
        {
            int NumOfLectures = context.lectures.Where(l => l.CourseID == CrsID && l.isDeleted == false).Count();
            return NumOfLectures;
        }
        #endregion

        #region Add Lecture
        public void Insert(Lecture lctFromReq)
        {
            context.lectures.Add(lctFromReq);
            SaveData();
        }
        #endregion

        #region Delete Lecture
        public void Delete(int id)
        {
            Lecture? lecture = context.lectures.FirstOrDefault(c => c.ID == id && c.isDeleted == false);
            if (lecture != null)
            {
                lecture.isDeleted = true;
                SaveData();
            }
        }
        #endregion
        #endregion


        //Get today's lecture by student id
        public List<VMLectureSchedule> GetTodayLectureByStudentId(int StudentID)
        {
            DateTime today = DateTime.Today;

            return context.Enrolls
                .Where(r => r.StudentID == StudentID)
                .SelectMany(r => r.Course.Lecture
                    .Where(l => l.LectureTime.Date == today)
                    .Select(l => new VMLectureSchedule
                    {
                        ID = l.ID,
                        Title = l.Title,
                        LectureTime = l.LectureTime,
                        CourseName = l.Course.Name,
                        Description = l.Description
                    }))
                .OrderByDescending(l => l.LectureTime)
            .ToList();
        }

        public List<VMLectureSchedule> GetLateLectures(int StudentID)
        {
            DateTime today = DateTime.Today;

            return context.Enrolls
                .Where(r => r.StudentID == StudentID)
                .SelectMany(r => r.Course.Lecture
                    .Where(l => l.LectureTime.Date < today && 
                                (context.Attends
                                .FirstOrDefault(a=>a.StudentID == StudentID && a.LectureID == l.ID)) == null)
                    .Select(l => new VMLectureSchedule
                    {
                        ID = l.ID,
                        Title = l.Title,
                        LectureTime = l.LectureTime,
                        CourseName = l.Course.Name,
                        Description = l.Description
                    }))
                .OrderByDescending(l => l.LectureTime)
            .ToList();
        }

        // all methods need to be tested

        public List<Lecture> GetAllCourseLectures(int CourseID) {
            return context.lectures.Where(l=>l.CourseID==CourseID).ToList(); 
        }

        
        public VMLectureDetails GetLectureVMByID(int id,int StudentID)
        {
            return context.lectures.Select(l => new VMLectureDetails
            {
                ID = l.ID,
               Comments= unitOfWork.CommentRepo.GetCommentsByLectureId(id),
               CourseName=l.Course.Name,
               Description=l.Description,
               LectureTime=l.LectureTime,
               Title=l.Title,
               VideoURL=l.VideoURL,
                ViewsCount = l.Attend
                       .Where(a => a.StudentID == StudentID && a.LectureID == id)
                       .Select(a => (int?)a.ViewsCount)
                       .FirstOrDefault() ?? 0
            }).FirstOrDefault(LVM => LVM.ID == id);
        }
        //need edit
        public VMLectureWithInstructorCourses GetLectureWithCourseList(int LectureId, int InstructorID)
        {
            List<VMCourseList> InstCourseList = context.Courses.Select(c=>new VMCourseList {
            ID=c.ID,
            Name=c.Name,
            }).ToList();

            return context.lectures.Select(l => new VMLectureWithInstructorCourses
            {
                ID = l.ID,
                Comments = l.Comment,
                CourseID = l.Course.ID,
                Description = l.Description,
                LectureTime = l.LectureTime,
                Title = l.Title,
                VideoURL = l.VideoURL,
                InstructorCourses = InstCourseList
                }).FirstOrDefault(LVM => LVM.ID == LectureId);
            }


        public List<VMLectureSchedule> GetStudentLectureSchedual(int StudentID)
        {
            DateTime today = DateTime.Today;
            DateTime nextWeek = today.AddDays(7);

            return context.Enrolls
                .Where(r => r.StudentID == StudentID)
                .SelectMany(r => r.Course.Lecture
                    .Where(l => l.LectureTime >= today &&
                                l.LectureTime <= nextWeek)
                    .Select(l => new VMLectureSchedule
                    {
                        ID = l.ID,
                        Title = l.Title,
                        LectureTime = l.LectureTime,
                        CourseName = l.Course.Name,
                    }))
                .OrderByDescending(l => l.LectureTime)
            .ToList();
        }

        public void Delete(Lecture Entity)
        {
            context.lectures.Remove(Entity);
        }

        public List<Lecture> GetAll()
        {
            throw new NotImplementedException();
        }

        public Lecture GetByID(int id)
        {
            return context.lectures.FirstOrDefault(d => d.ID == id);
        }




        public void SaveData()
        {
            context.SaveChanges();
        }
        
        public void Update(int id, Lecture Entity)
        {
           // Lecture l = GetByID(id);
            context.Update(Entity);
        }

        public LectureDetailsVM LctDetails(int LctID)
        {
            throw new NotImplementedException();
        }
    }
}