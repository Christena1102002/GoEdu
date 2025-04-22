using GoEdu.Data;
using GoEdu.Models;
using GoEdu.ViewModel;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

namespace GoEdu.Controllers
{
 //   [Authorize]
    public class CourseController : Controller
    {

        UnitOfWork unitOfWork;
        //int userId;
        //string imageUrl;
      //  string userName;
        public CourseController(UnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
            // userId = int.Parse(User.FindFirst("UserID")?.Value);
             //imageUrl = User.FindFirst("UserImage")?.Value;
             //userName = User.FindFirst("UserName")?.Value;

        }


        #region Mark Section

        #region  Get Instructor Courses
        [Authorize(Roles = "Instructor")]
        public IActionResult GetInsCourses()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int userId = int.Parse(userIdClaim.Value);

                var courses = unitOfWork.CourseRepo.GetIstructorCourses(userId);

                if (courses == null)
                {
                    return NotFound();
                }
                ViewData["NumOfAllStudent"] = unitOfWork.CourseRepo.GetInsStudentCount(userId);
                ViewData["NumOfCourses"] = unitOfWork.CourseRepo.GetInsCourseCount(userId);
                return View(courses);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region New Course
        [Authorize(Roles = "Instructor")]
        [HttpGet]
        public IActionResult NewCourse()
        {
            return View();
        }
        [Authorize(Roles = "Instructor")]
        [HttpPost]
        public IActionResult SaveCourses(AddCourseWithInstructorVM newCrs)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int userId = int.Parse(userIdClaim.Value);
                if (ModelState.IsValid == true)
                {
                    try
                    {
                        newCrs.InsID = userId;
                        unitOfWork.CourseRepo.SaveNew(newCrs);
                        TempData["CourseCreated"] = "تم الإضافة بنجاح!";
                        return RedirectToAction("GetInsCourses");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message.ToString());
                    }
                }
                return View("NewCourse", newCrs);
            }
            return RedirectToAction("Index", "Home");
        }


        #endregion

        #region Edit Course
        [Authorize(Roles = "Instructor")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            AddCourseWithInstructorVM course = unitOfWork.CourseRepo.EditCourse(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        public IActionResult SaveEdit(AddCourseWithInstructorVM crsFromReq)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int userId = int.Parse(userIdClaim.Value);
                if (ModelState.IsValid)
                {
                    try
                    {
                        crsFromReq.InsID = userId;
                        unitOfWork.CourseRepo.SaveEdit(crsFromReq);
                        TempData["CoursEdited"] = "تم التعديل بنجاح!";
                        return RedirectToAction("GetInsCourses");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message.ToString());
                    }
                }
                return View("Edit", crsFromReq);
            }
            return RedirectToAction("Index", "Home") ;
        }
        #endregion

        #region Delete Course
        [Authorize(Roles = "Instructor")]

        public IActionResult DeleteCourse(int id)
        {
            unitOfWork.CourseRepo.Delete(id);
            TempData["Deleted"] = "تم الحذف بنجاح !";

            return RedirectToAction("GetInsCourses");
        }
        #endregion
        [Authorize(Roles = "Instructor")]

       
        public IActionResult CheckPrice(double CrsPrice)
        {
            if (CrsPrice >= 50)
            {
                return Json(true);
            }
            return Json(false);
        }

        #endregion

        // ********************* Not Working **************************
        //public IActionResult CourseInsDetails(int id)
        //{
        //    return View();

        //}

        //public IActionResult Index(string searchQuery, string? filterBy, string? nameAccourdFilter)
        //{
        //    var courses = unitOfWork.CourseRepo.GetAll();

        //    //var courses = courseRepository.GetAll();
        //    List<Course> courslist;

        //    if (!string.IsNullOrEmpty(searchQuery))
        //    {
        //        courses = unitOfWork.CourseRepo.search(searchQuery);// courses.Where(c => c.Name.Contains(searchQuery)).ToList();
        //    }
        //    else if (!string.IsNullOrEmpty(filterBy) && !string.IsNullOrEmpty(nameAccourdFilter))
        //    {

        //        courses = unitOfWork.CourseRepo.FilterCourses(filterBy, nameAccourdFilter);
        //        //courses = courseRepository.FilterCourses(filterBy,nameAccourdFilter);


        //        //  courses = unitOfWork.CourseRepo.FilterCourses(filterBy, NameOfCourse);

        //    } }


        // ICourseRepository courseRepository;

        [AllowAnonymous]
        public IActionResult Index(string searchQuery, string? filterBy, string? nameAccourdFilter)
        {
            var courses = unitOfWork.CourseRepo.GetAll();
           
            if (!string.IsNullOrEmpty(searchQuery))
            {
                courses = unitOfWork.CourseRepo.search(searchQuery);
            }
            else if (!string.IsNullOrEmpty(filterBy) && !string.IsNullOrEmpty(nameAccourdFilter))
            {
                courses = unitOfWork.CourseRepo.FilterCourses(filterBy, nameAccourdFilter);
            }

            return View("Index", courses);
        }


        [Authorize(Roles = "Student")]

        //public IActionResult Details(int id)
        //{
        //    var Course = unitOfWork.CourseRepo.GetByID(id);

        //    if (Course == null)
        //    {
        //        return NotFound();
        //    }
        //    return View("Details", Course);
        //}
        //[Authorize(Roles = "Student")]?????anonymous???????
        [HttpGet]
        public IActionResult GetAllWithIns()
        {

            var courses = unitOfWork.CourseRepo.GetAllcourses();

           
            return View("GetAllWithIns", courses);
        }
        //public IActionResult filtered(string? instructorName,)
        //{
        //    var filteredCourses = unitOfWork.CourseRepo.FilterCourses(instructorName);
        //    return View("filtered", filteredCourses);
        //}
        //course id
        //public IActionResult CourseDetails(int id)
        //{
        //    var courseDetails= unitOfWork.CourseRepo.GetCourseWithLectures(id);
        //    if (courseDetails == null)

        [Authorize(Roles = "Student")]
        [HttpGet]
        public IActionResult CourseDetails(int id)
        {
            var courseDetails= unitOfWork.CourseRepo.GetCourseWithLectures(id); 
            if(courseDetails == null)
            {
                return NotFound("Course not found");
            }
            Console.WriteLine("✅ Course Found: " + courseDetails.CourseName);
           // return Content("Course ID is: " + id);
           return View("CourseDetails", courseDetails);
        }
        //when student register in a course
        [Authorize(Roles = "Student")]

        [HttpGet]
        public IActionResult EnrollInCourse(int courseID)
        {
            //get instructor id
            //get record if found =>you are already registerd
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int userId = int.Parse(userIdClaim.Value);
                int InstructorID = unitOfWork.CourseRepo.GetByID(courseID).InstructorID;
                Enroll enrolled = unitOfWork.EnrollRepo.GetBy3IDs(courseID, InstructorID, userId);
                if (enrolled != null)
                {
                    // Student already enrolled
                    ViewBag.Message = "You are already enrolled in this course.";
                    return RedirectToAction("CourseDetails", new { id = courseID });
                }

                Enroll enroll = new Enroll
                {
                    CourseID = courseID,
                    InstructorID = InstructorID,
                    StudentID = userId,
                    Data = DateTime.Now
                };
                unitOfWork.EnrollRepo.Insert(enroll);
                unitOfWork.save();

                return RedirectToAction("CourseDetails", new { id = courseID });
            }
            return RedirectToAction("Index", "Home");

        }


    }
}
