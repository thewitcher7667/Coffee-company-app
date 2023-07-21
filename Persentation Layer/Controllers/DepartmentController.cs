using Business_Logic_Layer.Services.Contracts;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Persentation_Layer.Controllers
{
    [Authorize(Roles ="manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmenServices _services;

        public DepartmentController(IDepartmenServices services)
        {
            _services = services;
        }

        [Route("/Home/Department")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        // GET api/<DepartmentController>/5
        [HttpGet]
        public async Task<List<Department>> GetAll()
        {
            return await _services.GetDepartments();
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public Department Get(int id)
        {
            return _services.GetDepartmentById(id);
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public async Task<ActionResult<Department>> Post([FromBody] Department value)
        {
            if(ModelState.IsValid)
            {
                return await _services.AddDepartment(value);
            }
            return BadRequest();
        }

        // PUT api/<DepartmentController>/5
        [HttpPut]
        public ActionResult<bool> Put([FromBody] Department value)
        {
            if (ModelState.IsValid)
            {
                return _services.UpdateDepartment(value);
            }
            return BadRequest();
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _services.DeleteDepartment(id);
        }
    }
}
