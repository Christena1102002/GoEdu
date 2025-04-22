using System.Collections.Generic;
using GoEdu.Data;
using GoEdu.Hubs;
using GoEdu.Models;
using GoEdu.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GoEdu.Controllers
{
    

    public class LectureController : Controller
    {

        UnitOfWork UnitOfWork;
        private IHubContext<CommentHub> hubContext;
        //int UserId;
        public LectureController(UnitOfWork unitOfWork, IHubContext<CommentHub> hubContext)
        {
            this.UnitOfWork = unitOfWork;
            this.hubContext = hubContext;
           // UserId = int.Parse(User.FindFirst("UserID")?.Value);

        }

        #region Mark Section
        #region All Lecture Course
        
        //[
        //(Roles = "Instructor")]
        [HttpGet]
        public IActionResult GetLectures(int id)//CourseId instead of id ???????
        {
            List<LectureWithInstructorVM> lectures = UnitOfWork.LectureRepository.GetLectureCourses(id);

            if (lectures == null)
            {
                return NoContent();
            }
            ViewData["LectureCount"] = UnitOfWork.LectureRepository.LectureCount(id);
            return View(lectures);
        }
        #endregion

        #region New Lecture
        [Authorize(Roles = "Instructor")]
        #region Add Lecture
        public IActionResult NewLecture(int CrsID)
        {

            AddOrEditLectureVM CrsId = new AddOrEditLectureVM() { CourseID = CrsID };
            return View(CrsId);
        }


        [HttpPost]
        public IActionResult SaveNew(AddOrEditLectureVM lctFromReq)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    Lecture lecture = new Lecture
                    {
                        Title = lctFromReq.Title,
                        VideoURL = lctFromReq.VideoURL,
                        LectureTime = lctFromReq.LectureTime,
                        Description = lctFromReq.Description,
                        isDeleted = false,
                        CourseID = lctFromReq.CourseID,
                        ImageUrl=UnitOfWork.CourseRepo.GetByID(lctFromReq.CourseID).ImageUrl,
                    };
                    UnitOfWork.LectureRepository.Insert(lecture);
                    TempData["SuccessMessage"] = "تم إضافة المحاضرة بنجاح!";
                    return RedirectToAction("GetLectures", new { id = lctFromReq.CourseID });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message.ToString());
                }
            }
            return View("NewLecture", lctFromReq);
        }

        #endregion
        #endregion

        #region Delete Lecture
        [Authorize(Roles = "Instructor")]

        public IActionResult DeleteLecture(int id, int CrsID)
        {
            UnitOfWork.LectureRepository.Delete(id);
            TempData["Deleted"] = "تم الحذف بنجاح !";

            return RedirectToAction("GetLectures", new { id = CrsID });
        }
        #endregion
        #region Edit Lecture

        [HttpGet]
        public IActionResult EditLecture(int id)
        {
            Lecture lecture = UnitOfWork.LectureRepository.GetByID(id);
            AddOrEditLectureVM lectureVM = new AddOrEditLectureVM()
            {
                LctID = lecture.ID,
                Title = lecture.Title,
                LectureTime = lecture.LectureTime,
                Description = lecture.Description,
                CourseID = lecture.CourseID,
                VideoURL=lecture.VideoURL
            };
            return View(lectureVM);
        }


        [HttpPost]
        public IActionResult SaveEdit(AddOrEditLectureVM LctFromReq)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork.LectureRepository.SaveEdit(LctFromReq);
                    TempData["CoursEdited"] = "تم التعديل بنجاح!";
                    return RedirectToAction("GetLectures", new { id = LctFromReq.CourseID });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message.ToString());
                }
            }
            return View("EditLecture", LctFromReq);
        }

        #endregion
        #endregion


        //[HttpGet]


        //public IActionResult LectureDetails(int id)
        //{
        //    Lecture lecture = UnitOfWork.LectureRepository.GetByID(id);
        //    return View(lecture);
        //}
        [Authorize(Roles = "Student")]
        [HttpGet]
        public IActionResult LectureDetails(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int UserId = int.Parse(userIdClaim.Value);
                ViewBag.userId = UserId;

                VMLectureDetails lecture = UnitOfWork.LectureRepository.GetLectureVMByID(id, UserId);
                Attend attend = UnitOfWork.AttendRepo.GetBy2Ids(UserId, id);
                if (attend == null)
                {
                    attend = new Attend();
                    attend.StudentID = UserId;
                    attend.LectureID = id;
                    attend.ViewsCount += 1;
                    UnitOfWork.AttendRepo.Insert(attend);
                }
                else
                {
                    if (UnitOfWork.CourseRepo.GetByID(UnitOfWork.LectureRepository.GetByID(id).CourseID).MaxViews > attend.ViewsCount)
                        attend.ViewsCount += 1;
                    else
                    {
                        ModelState.AddModelError("", "You Have reached maximum Views");
                        return RedirectToAction("StudentDashBoard", "Student");
                    }
                }
                return View(lecture);
            }
            return RedirectToAction("Index", "Home");
        }
        
        //[Authorize(Roles = "Instructor")]
        //[HttpGet]
        //public IActionResult EditLecture(int id)
        //{
        //    var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
        //    if (userIdClaim != null)
        //    {
        //        int UserId = int.Parse(userIdClaim.Value);
        //        VMLectureWithInstructorCourses lecture = UnitOfWork.LectureRepository.GetLectureWithCourseList(id, UserId);

        //        return View(lecture);
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
        //[Authorize(Roles = "Instructor")]

        //[HttpPost]
        //public IActionResult EditLecture(VMLectureWithInstructorCourses lectureVM)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        Lecture lecture = UnitOfWork.LectureRepository.GetByID(lectureVM.ID);
        //        if (lecture != null)
        //        {
        //            lecture.Description = lectureVM.Description;
        //            lecture.ID = lectureVM.ID;
        //            lecture.Title = lectureVM.Title;
        //            lecture.LectureTime = lectureVM.LectureTime;
        //            lecture.VideoURL = lectureVM.VideoURL;

        //            try
        //            {
        //                UnitOfWork.LectureRepository.Update(lecture.ID, lecture);
        //                UnitOfWork.save();
        //                //need edit student id 
        //                return RedirectToAction("LectureDetails", new { id = lecture.ID, StudentID = 1 });
        //            }
        //            catch (Exception ex)
        //            {
        //                ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Lecture not found.");
        //            lectureVM.InstructorCourses = UnitOfWork.LectureRepository.GetLectureWithCourseList(lectureVM.ID, 1).InstructorCourses;
        //            return View(lectureVM);
        //        }
        //    }
        //    //need edit instructor id
        //    lectureVM.InstructorCourses = UnitOfWork.LectureRepository.GetLectureWithCourseList(lectureVM.ID, 1).InstructorCourses;
        //    return View(lectureVM);
        //}

        [Authorize(Roles = "Student")]
        [HttpGet]
        public IActionResult LectureSchedule()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int UserId = int.Parse(userIdClaim.Value);
                List<VMLectureSchedule> lectures = UnitOfWork.LectureRepository.GetStudentLectureSchedual(UserId);

                return View(lectures);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}

