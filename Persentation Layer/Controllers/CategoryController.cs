using Business_Logic_Layer.Services;
using Business_Logic_Layer.Services.Contracts;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Persentation_Layer.Controllers
{
    [Authorize(Roles = "manager,restaurant")]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryServices _services;

        public CategoryController(ICategoryServices services)
        {
            _services = services;
        }

        [HttpGet]
        [Route("/Home/Category")]
        public IActionResult Index()
        {
            return View();
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<List<Category>> Get()
        {
            return await _services.GetAllCategories();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _services.GetCategory(id);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] Category value)
        {
            if (ModelState.IsValid)
            {
                return await _services.AddCategory(value);
            }
            return BadRequest();
        }

        // PUT api/<RestaurantController>/5
        [HttpPut]
        public ActionResult<bool> Put([FromBody] Category value)
        {
            if (ModelState.IsValid)
            {
                return _services.EditCategory(value);
            }
            return BadRequest();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _services.DeleteCategory(id);
        }
    }
}
