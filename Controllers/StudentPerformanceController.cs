using GoEdu.Data;
using GoEdu.Models;
using GoEdu.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoEdu.Controllers
{
    public class StudentPerformanceController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public StudentPerformanceController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Authorize(Roles = "Student")]
        public IActionResult ShowStudentPerformanceByStudentId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int studentId = int.Parse(userIdClaim.Value);
                List<VMStudentPerformance> studentPerfomances = unitOfWork.StudentPerformanceRepo.GetStudentPerformanceByStudentId(studentId);
                return View(studentPerfomances);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Instructor")]
        public IActionResult ShowStudentPerformanceByLectureId(int lectureId)
        {
            //int instructorId = int.Parse(User.FindFirst("UserID")?.Value);
            List<VMStudentPerformance> studentPerfomancesByInstructor = unitOfWork.StudentPerformanceRepo.GetStudentPerformanceByLectureId(lectureId);

            Lecture lecture = unitOfWork.LectureRepository.GetByID(lectureId);
            ViewData["LectureName"] = lecture.Title;
            ViewData["LectureTime"] = lecture.LectureTime;
            ViewData["CourseName"] = unitOfWork.CourseRepo.GetCourseByLectureId(lectureId).Name;

            return View(studentPerfomancesByInstructor);
        }

    }
}

