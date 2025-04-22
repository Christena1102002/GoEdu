using GoEdu.Data;
using GoEdu.Models;
using GoEdu.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoEdu.Controllers
{
    public class QuestionController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        //int UserId;
        public QuestionController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            //UserId = int.Parse(User.FindFirst("UserID")?.Value);

        }
        [Authorize(Roles = "Instructor")]

        [HttpGet]
        public IActionResult AddAndShowQuestions(int id)
        {
            VMQuestionWithQuestions questions = unitOfWork.QuestionRepo.GetAndAddQustionListByLectureId(id);

            return View(questions);
        }
        [Authorize(Roles = "Instructor")]

        [HttpPost]
        public IActionResult AddAndShowQuestions(VMQuestionWithQuestions QuestionFromView)
        {
            if (ModelState.IsValid)
            {
                Question CheckQuestion = unitOfWork.QuestionRepo.GetQuestionByContent(QuestionFromView.Content, QuestionFromView.LectureId);
                if (CheckQuestion == null)//|| CheckQuestion.LectureId != QuestionFromView.LectureId)
                {
                    try
                    {
                        Question question = new Question
                        {
                            Content = QuestionFromView.Content,
                            LectureId = QuestionFromView.LectureId,
                            ModelAnswer = QuestionFromView.ModelAnswer
                        };

                        unitOfWork.QuestionRepo.Insert(question);
                        unitOfWork.save();

                        int questionId = unitOfWork.QuestionRepo.GetQuestionByContent(question.Content, question.LectureId).Id;

                        unitOfWork.OptionRepo.Insert(new Option
                        {
                            Content = QuestionFromView.Option1,
                            QuestionId = questionId,
                        });
                        unitOfWork.OptionRepo.Insert(new Option
                        {
                            Content = QuestionFromView.Option2,
                            QuestionId = questionId,
                        });
                        unitOfWork.OptionRepo.Insert(new Option
                        {
                            Content = QuestionFromView.Option3,
                            QuestionId = questionId,
                        });
                        unitOfWork.OptionRepo.Insert(new Option
                        {
                            Content = QuestionFromView.Option4,
                            QuestionId = questionId,
                        });
                        unitOfWork.save();

                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.InnerException.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "هذا السؤال موجود بالغعل");
                }

            }

            return RedirectToAction("AddAndShowQuestions", QuestionFromView.LectureId);
        }
        [HttpGet]

        public IActionResult ShowLectureQuestions(int LectureID)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int UserId = int.Parse(userIdClaim.Value);
                StudentPerformance studentPerformance = unitOfWork.StudentPerformanceRepo.GetBy2IDs(UserId, LectureID);
                if (studentPerformance != null)
                {
                    ModelState.AddModelError("", "لقد امتحنت هذا الامتحان مسبقا!");
                    return RedirectToAction("LectureDetails", "Lecture", new { id = LectureID });//????????????????????
                }
                List<VMQuestionAnswer> questions = unitOfWork.QuestionRepo.GetQuestionsByLectureID(LectureID);
                ViewBag.LectureId = LectureID;
                return View(questions);
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Student")]

        [HttpPost]
        public IActionResult ShowLectureQuestions(List<VMQuestionAnswer> questionAnswers)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim != null)
            {
                int UserId = int.Parse(userIdClaim.Value);
                int grad = 0;
                foreach (var ans in questionAnswers)
                {

                    if (ans.StudentAnswer == ans.ModelAnswer)
                    {
                        grad++;
                    }

                    var answer = new Answer
                    {
                        QuestionId = ans.QuestionId,
                        StudentId = UserId,
                        StudentAnswer = ans.StudentAnswer
                    };


                    unitOfWork.AnswerRepo.Insert(answer);

                }
                int percent = 100 * grad / questionAnswers.Count();
                StudentPerformance studentPerformance = new StudentPerformance
                {
                    LectureId = questionAnswers[0].LectureId,
                    StudentId = UserId,
                    Grade = percent
                };
                unitOfWork.StudentPerformanceRepo.Insert(studentPerformance);
                unitOfWork.save();
                return RedirectToAction("LectureDetails", "Lecture", new { id = questionAnswers[0].LectureId, StudentID = questionAnswers[0].StudentId });
            }
            return RedirectToAction("Index", "Home");
        }

        //public IActionResult Index()
        //{

        //    return View("Index", "Home");
        //}


    }
}
