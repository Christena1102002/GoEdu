using GoEdu.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoEdu.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        
            RoleManager<IdentityRole> roleManager;
            public RoleController(RoleManager<IdentityRole> roleManager)
            {
                this.roleManager = roleManager;
            }
            [AllowAnonymous]
            public IActionResult test()
            {
                return Content("welcome");
            }

            public IActionResult New()
            {
                //if(User.IsInRole("Admin"))

                return View("New");
            }
            [HttpPost]
            public async Task<IActionResult> New(VMRole roleFromReq)
            {
                if (ModelState.IsValid)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = roleFromReq.RoleName;
                    IdentityResult result = await roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return Content("Success");
                    }
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                return View("New");
            }
        
    }
}
