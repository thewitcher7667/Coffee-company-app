using Business_Logic_Layer.Services.Contracts;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Persentation_Layer.Controllers
{
    [Authorize(Roles ="manager")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [Route("/Home/Users")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<User>> Get()
        {
            List<User> users = await _userServices.GetUsers();
            return users.Select(x=> new User { 
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                Department = x.Department,
            }).ToList();
        }

        // GET api/<UsersController>/5
        [HttpGet("GetById/{id}")]
        public User Get(string id)
        {
            return  _userServices.GetUserById(id);
        }

        [HttpGet("GetByEmail/{email}")]
        public User GetByEmail(string email)
        {
            return _userServices.GetUserByEmail(email);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User value)
        {
            if(ModelState.IsValid)
            {
                //check if department is exist and return error
                try
                {
                    return await _userServices.AddUser(value);
                }
                catch(Exception ex)
                {
                    return Problem(ex.Message, statusCode: (int)HttpStatusCode.BadRequest);
                }
            }
            string allErrors = "";
            ModelState.Values.SelectMany(v => v.Errors).ToList().ForEach(x => allErrors += $" {x.ErrorMessage}");

            //want to return errors from model state error
            return Problem(allErrors,statusCode:(int)HttpStatusCode.BadRequest);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] User value)
        {
            try
            {
                return  await _userServices.UpdatUser(value);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: (int)HttpStatusCode.BadRequest);

            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return _userServices.DeleteUser(id);
        }
    }
}
