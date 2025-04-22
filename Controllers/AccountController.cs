using System.Security.Claims;
using GoEdu.Data;
using GoEdu.Models;
using GoEdu.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoEdu.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UnitOfWork unitOfWork;
        RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, UnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.unitOfWork = unitOfWork;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(VMRegister userFromRequest)
        {

            if (ModelState.IsValid )
            {

                //add db
                ApplicationUser userApp = new ApplicationUser();
                userApp.UserName = userFromRequest.UserName;
                userApp.PasswordHash = userFromRequest.Password;
                //userApp.Address = userFromREquest.Address;
                IdentityResult result = await userManager.CreateAsync(userApp, userFromRequest.Password);
                if (result.Succeeded)
                {
                    if (userFromRequest.IsInstructor)
                    {
                        if (!await roleManager.RoleExistsAsync("Instructor"))
                        {
                            IdentityRole role = new IdentityRole();
                            role.Name = "Instructor";
                            await roleManager.CreateAsync(role);
                        }
                        await userManager.AddToRoleAsync(userApp, "Instructor");//add User To Role
                        await signInManager.SignInAsync(userApp, false);
                        //get id
                        //insert in instructor table 
                        Instructor instructor = new Instructor
                        {
                            ApplicationUserId = userApp.Id,
                            Address = userFromRequest.Address,
                            Age = userFromRequest.Age,
                            ImageUrl = userFromRequest.ImageUrl,
                            Name = userFromRequest.UserName,
                            Phone = userFromRequest.Phone,
                            Email = userFromRequest.Email,

                        };
                        unitOfWork.InstructorRepo.Insert(instructor);
                        unitOfWork.save();
                    }
                    else
                    {
                        if (!await roleManager.RoleExistsAsync("Student"))
                        {
                            IdentityRole role = new IdentityRole();
                            role.Name = "Student";
                            await roleManager.CreateAsync(role);
                        }
                        await userManager.AddToRoleAsync(userApp, "Student");
                        await signInManager.SignInAsync(userApp, false);
                        //student obj
                        Student student = new Student
                        {
                            ApplicationUserId = userApp.Id,
                            Address = userFromRequest.Address,
                            Email = userFromRequest.Email,
                            Name = userFromRequest.UserName,
                            Age = userFromRequest.Age,
                            ImageUrl = userFromRequest.ImageUrl,

                            StudentPhone = userFromRequest.Phone
                        };
                        unitOfWork.StudentRepo.Insert(student);
                        unitOfWork.save();
                    }

                    return RedirectToAction("Logout", "Account");

                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", userFromRequest);
        }

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(VMLogin userFRomREquest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appFromDb =
                    await userManager.FindByNameAsync(userFRomREquest.UserName);
                if (appFromDb != null)
                {
                    bool found = await userManager.CheckPasswordAsync(appFromDb, userFRomREquest.Password);
                    if (found)
                    {
                        List<Claim> claims = new List<Claim>();
                        if (await userManager.IsInRoleAsync(appFromDb,"Instructor")) {
                            Instructor userFromDb = unitOfWork.InstructorRepo.GetUserByFK(appFromDb.Id);
                            claims.Add(new Claim("UserID", userFromDb.ID.ToString()));
                            claims.Add(new Claim("UserImage", userFromDb.ImageUrl));
                            claims.Add(new Claim("UserName", userFromDb.Name));
                            await signInManager.SignInWithClaimsAsync(appFromDb, userFRomREquest.RememberMe, claims);

                            return RedirectToAction("DashBoard", "Instructor");

                        }
                        else
                        {
                            Student userFromDb = unitOfWork.StudentRepo.GetUserByFK(appFromDb.Id);
                            claims.Add(new Claim("UserID", userFromDb.ID.ToString()));
                            claims.Add(new Claim("UserImage", userFromDb.ImageUrl));
                            claims.Add(new Claim("UserName", userFromDb.Name));
                            await signInManager.SignInWithClaimsAsync(appFromDb, userFRomREquest.RememberMe, claims);
                            return RedirectToAction("DashBoard", "Student");

                        }

                       }
                }
                ModelState.AddModelError("", "Invalid Account");
            }
            return View("Login", userFRomREquest);
        }


        #endregion

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
