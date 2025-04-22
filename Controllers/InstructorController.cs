using GoEdu.Data;
using GoEdu.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoEdu.Controllers
{
    public class InstructorController : Controller
    {
        private readonly UnitOfWork unitOfWork;
       // int UserId;
        public InstructorController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            //UserId = int.Parse(User.FindFirst("UserID")?.Value);

        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        [Authorize(Roles = "Instructor")]

        [HttpGet]
        public IActionResult DashBoard()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int UserId = int.Parse(userIdClaim.Value);
                VMInstructorDashBoard dashBoard = new VMInstructorDashBoard
                {
                    InstructorId = UserId,
                    TotalStudents = unitOfWork.StudentRepo.GetStudentsByInstructor(UserId).Count(),
                    TotalCourses = unitOfWork.CourseRepo.CoursesByInstructor(UserId).Count(),
                    TotalViews = unitOfWork.AttendRepo.LecturesTotalViewsByInstructorId(UserId),
                    TopTenPerformances = unitOfWork.StudentPerformanceRepo.TopTenByInstructorId(UserId),
                };

                return View(dashBoard);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
