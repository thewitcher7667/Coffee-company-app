using Business_Logic_Layer.Services.Contracts;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Persentation_Layer.Controllers
{
    [Route("/[controller]")]
    public class LoginController : Controller
    {
        private readonly IDepartmenServices _departmenServices;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public LoginController(UserManager<User> userManager
                            , SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager
                            , IDepartmenServices departmenServices)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            _departmenServices = departmenServices;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            returnUrl ??= "/Home";
            ViewData["ReturnUrl"] = returnUrl;
            return View("~/Views/Login/Login.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> Login(string returnUrl,[FromForm]LoginModel model)
        {
            if(ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnUrl;
                var user = await userManager.FindByNameAsync(model.Name);
                if (user == null)
                {
                    ModelState.AddModelError("", "Check your password or email");
                    return View("~/Views/Login/Login.cshtml");
                }
                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password,true,false);
                if (result.Succeeded)
                {
                    var department = _departmenServices.GetDepartmentById(user.DepartmentId);
                    string departmentName = department.Name == null ? "worker" : department.Name;
                    departmentName = departmentName.ToLower();
                    bool roleExisted = await roleManager.RoleExistsAsync(departmentName);

                    if(!roleExisted)
                    {
                        await roleManager.CreateAsync(new IdentityRole(departmentName));
                    }
                    await userManager.AddToRoleAsync(user, departmentName);
                    return Redirect(returnUrl);
                }
                ModelState.AddModelError("", "Check your password or email");
            }
            return View("~/Views/Login/Login.cshtml");
        }

        [Authorize]
        [HttpGet]
        [Route("/Home")]
        public IActionResult Home()
        {
            return View("~/Views/Home.cshtml");
        }

        [Authorize]
        [HttpGet]
        [Route("/Logout")]
        public async Task<IActionResult> Logout()
        {
            
           await signInManager.SignOutAsync();
           return Redirect("/Login");
        }
    }
}
