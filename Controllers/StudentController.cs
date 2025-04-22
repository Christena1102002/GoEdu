using GoEdu.Data;
using GoEdu.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoEdu.Models;
using Microsoft.AspNetCore.Authorization;

namespace GoEdu.Controllers
{
    public class StudentController : Controller
    {
        private UnitOfWork unitOfWork;
        //int UserIdclaim;
        public StudentController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
           // UserId = int.Parse(User.Claims.FirstOrDefault(c=>c.Type == "UserID")?.Value ?? "0");//.FindFirst("UserID")?.Value);

        }
        [Authorize(Roles = "Student")]
        [HttpGet]
        public IActionResult DashBoard()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int userId = int.Parse(userIdClaim.Value);

                VMStudentDashBoard Dashboard = new VMStudentDashBoard
                {


                    TodayLectures = unitOfWork.LectureRepository.GetTodayLectureByStudentId(userId),
                    LateLectures = unitOfWork.LectureRepository.GetLateLectures(userId),
                    Courses = unitOfWork.CourseRepo.CoursesByStudentId(userId)
                };
                //nessecary need to send error from action "LectureDetails" in lecture controller
                if (ModelState.IsValid) { }
                return View(Dashboard);
            }
            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Instructor")]
        [HttpGet]
        public IActionResult AllStudentsByInstructor()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int userId = int.Parse(userIdClaim.Value);
                StudentsCoursesVM StdVM = new();

                //all Students by instructor
                StdVM.Students = unitOfWork.StudentRepo.GetStudentsByInstructor(userId);

                // get courses by instructor
                StdVM.Courses = unitOfWork.CourseRepo.CoursesByInstructor(userId);
                return View(StdVM);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult FilterStudentsByCourse(int CourseId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int userId = int.Parse(userIdClaim.Value);
                StudentsCoursesVM StudentsfromView = new StudentsCoursesVM();
                if (CourseId != 0)
                {
                    // get students with a specific course
                    StudentsfromView.Students = unitOfWork.StudentRepo.GetStudentsByCourse(CourseId);
                    StudentsfromView.Courses = unitOfWork.CourseRepo.CoursesByInstructor(userId);
                }
                else
                {
                    return RedirectToAction("AllStudentsByInstructor");
                }
                return View("AllStudentsByInstructor", StudentsfromView);
            }
            return RedirectToAction("Index", "Home");
        }
        //get id from instructor view
        [Authorize(Roles = "Instructor")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            Student std = unitOfWork.StudentRepo.GetByID(id);
            return View(std);
        }
        [Authorize(Roles = "Student")]
        [HttpGet]
        public IActionResult StudentProfile()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int userId = int.Parse(userIdClaim.Value);
                Student std = unitOfWork.StudentRepo.GetByID(userId);
                return View(std);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
