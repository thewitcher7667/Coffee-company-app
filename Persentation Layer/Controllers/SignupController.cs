using Business_Logic_Layer.Services.Contracts;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Persentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public SignupController(IUserServices userServices,UserManager<User> userManager
                            , SignInManager<User> signInManager)
        {
            _userServices = userServices;
            this.userManager = userManager;
            this.signInManager = signInManager; 
        }

        [HttpPost(Name = "AddUser")]
        public async Task<ActionResult<User>> Signup(User user)
        {
            if(ModelState.IsValid)
            {
                //before return get the department name by joining usertables .groupjoin department table
                //check id department is valid and add model error 
                //throw exeption if error in logic
               return await _userServices.AddUser(user);
            }

            //return list of errors of model state

            return BadRequest();
        }
 
    }
}
