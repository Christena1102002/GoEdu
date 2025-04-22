using GoEdu.Data;
using GoEdu.Models;
using GoEdu.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;


namespace GoEdu.Repositories
{
    public class CourseRepository:ICourseRepository
    {
        GoEduContext Context;
        public CourseRepository(GoEduContext contxt)
        {
            Context = contxt;
        }


       #region Mark Section

        #region Get Instructor Courses
        public List<CourseWithInstructorVM> GetIstructorCourses(int insID)
        {
            List<CourseWithInstructorVM> crsVm = Context.Courses.Where(c => c.InstructorID == insID && c.isDeleted == false)
                .Select(c => new CourseWithInstructorVM()
                {
                    CrsID = c.ID,
                    CrsName = c.Name,
                    CrsPrice = c.Price,
                    CrsHours = c.Hours,
                    ImageUrl=c.ImageUrl,
                    CrsMinDegree = c.MinDegree,
                    CrsSemester = c.Semester,
                    CrsStage = c.StudentStage,
                    CrsLevel = c.CourseLevel,
                    NumOfLecture = Context.lectures.Where(l => l.CourseID == c.ID && l.isDeleted == false).Count(),
                    NumOfStudent = Context.Enrolls.Where(r => r.CourseID == c.ID && r.isDeleted == false).Count()
                }).AsNoTracking().ToList();
            return crsVm;
        }
        public int GetInsStudentCount(int InsID)
        {
            int Count = Context.Enrolls.Where(r => r.InstructorID == InsID && r.isDeleted == false).Count();
            return Count;
        }
        public int GetInsCourseCount(int InsID)
        {
            int Count = Context.Courses.Where(r => r.InstructorID == InsID && r.isDeleted == false).Count();
            return Count;
        }
        #endregion

        #region New Course
        public void SaveNew(AddCourseWithInstructorVM newCrs)
        {
            Course crs = new Course()
            {
                Name = newCrs.CrsName,
                Price = newCrs.CrsPrice,
                Hours = newCrs.CrsHours,
                Degree = 100,
                MinDegree = newCrs.CrsMinDegree,
                Semester = newCrs.CrsSemester,
                StudentStage = newCrs.CrsStage,
                CourseLevel = newCrs.CrsLevel,
                ImageUrl=newCrs.ImageUrl,
                isDeleted = false,
                InstructorID = newCrs.InsID
            };
            Context.Courses.Add(crs);
            SaveData();
        }

        #endregion

        #region Edit Course
        public AddCourseWithInstructorVM EditCourse(int id)
        {
            Course crsFromDB = GetByID(id);

            AddCourseWithInstructorVM crsVM = new AddCourseWithInstructorVM()
            {
                CrsID = crsFromDB.ID,
                CrsName = crsFromDB.Name,
                CrsPrice = crsFromDB.Price,
                CrsHours = crsFromDB.Hours,
                CrsMinDegree = crsFromDB.MinDegree,
                CrsSemester = crsFromDB.Semester,
                CrsStage = crsFromDB.StudentStage,
                CrsLevel = crsFromDB.CourseLevel,
            };
            return crsVM;
        }

        public void SaveEdit(AddCourseWithInstructorVM crsFromReq)
        {
            Course? crsFromDB = Context.Courses.FirstOrDefault(c => c.ID == crsFromReq.CrsID);

            crsFromDB.Name = crsFromReq.CrsName;
            crsFromDB.Price = crsFromReq.CrsPrice;
            crsFromDB.Hours = crsFromReq.CrsHours;
            crsFromDB.MinDegree = crsFromReq.CrsMinDegree;
            SaveData();
        }

        #endregion

        #region Delete Course
        public void Delete(int id)
        {
            Course? course = Context.Courses.FirstOrDefault(c => c.ID == id && c.isDeleted == false);
            if (course != null)
            {
                course.isDeleted = true;
                SaveData();
            }
        }
        #endregion

        #endregion


        public List<Course> CoursesByInstructor(int instructorId)
        {
            return Context.Courses.Where(c=>c.InstructorID == instructorId).ToList();
        }

        public void Delete(Course obj)
        {
            //Course course = GetByID(id);
            //Context.Remove(course);
            Context.Courses.Remove(obj);
        }

        public List<Course> GetAll()
        {
            List<Course> list = Context.Courses.Include(c => c.Instructor).ToList();
            return list;
        }

        public List<CourseViewModel> GetAllcourses()
        {
            var courses = Context.Courses
                .Select(c => new CourseViewModel
                {
                    ID = c.ID,
                    Name = c.Name,
                    Price = c.Price,
                    Hours = c.Hours,
                    MaxViews = c.MaxViews,

                    //CourseLevel = c.CourseLevel,
                    //Semester = c.Semester,
                    CourseLevel = c.CourseLevel,

                    InstructorName = c.Instructor.Name,


                }).ToList();
            return courses;
        }
        public Course GetByID(int id)
        {
            Course course = Context.Courses.Include(c=>c.Instructor).FirstOrDefault(c => c.ID == id);
            return course;
        }

        public void Insert(Course Obj)
        {
            Context.Courses.Add(Obj);
        }

        public void SaveData()
        {
            Context.SaveChanges();
        }

        public void Update(int id, Course obj)
        {
            var existingCourse = Context.Courses.FirstOrDefault(c => c.ID == id);
            if (existingCourse != null)
            {
                existingCourse.Name = obj.Name;
                existingCourse.Semester = obj.Semester;
                existingCourse.CourseLevel = obj.CourseLevel;
                existingCourse.Hours = obj.Hours;
                existingCourse.Price = obj.Price;
                Context.SaveChanges();
            }
        }

       public List<Course> FilterCourses(string filterBy, string nameAccourdFilter)
          {
                
                IQueryable<Course> coursesQuery = Context.Courses;
            if (!string.IsNullOrEmpty(filterBy)&& !string.IsNullOrEmpty(nameAccourdFilter))
            {
                if (filterBy == "instructorName")
                {
                    coursesQuery = coursesQuery.Where(c => c.Instructor.Name.ToLower().Contains(nameAccourdFilter.ToLower()));
                    //.Select(i => i.ID)
                   
                    //coursesQuery = coursesQuery.Where(c => instructorIds.Contains(c.InstructorID));
                }
                else if (filterBy == "courseName")
                {

                    coursesQuery = coursesQuery.Where(c => c.Name.Contains(nameAccourdFilter.ToLower()));
                }

            }
           
            
            return coursesQuery.ToList();

        }
      
        public List<Course> search(string searchQuery)
        {
            List<Course> courses = Context.Courses.ToList(); ;
            if (!string.IsNullOrEmpty(searchQuery))
            {
                courses = courses.Where(c => c.Name.Contains(searchQuery)).ToList();
            }
            return courses;
        }
        public CourseDetailsViewModel GetCourseWithLectures(int courseId)
        {
            var Result=Context.Courses.Where(c=>c.ID==courseId)
                .Select(c => new CourseDetailsViewModel
                {
                    CourseName = c.Name,
                    Lectures = c.Lecture.Select(l => new VMLectureDetails
                    {
                        Title = l.Title,
                         ID=l.ID,
                        VideoURL = l.VideoURL,
                        //ReleaseDate = l.LectureTime,
                        Description = l.Description,
                        Comments = l.Comment.Select(c => new VMComment
                        {
                            Content = c.Content,
                            Date = c.Date,
                            LectureId = c.LectureID,
                            UserId = c.UserID,
                            UserImageUrl = c.Student.ImageUrl,
                            UserName = c.Student.Name
                        }).ToList(),
                        //Question=l.Question,

                    }).ToList()
                }).FirstOrDefault();


            return Result;
        }
        public Course GetCourseByLectureId(int lectureId)
        {
            return Context.lectures.Where(l => l.ID == lectureId).Select(c => c.Course).FirstOrDefault();
        }

        public List<Course> CoursesByStudentId(int StudentId)
        {
            return Context.Enrolls.Where(s=>s.StudentID == StudentId).Select(e=>e.Course).ToList();
        }
    }
}
