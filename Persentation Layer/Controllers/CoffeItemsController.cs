using Business_Logic_Layer.Services.Contracts;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Persentation_Layer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeItemsController : Controller
    {
        private readonly IRestaurantServices _restaurantServices;
        private readonly ICategoryServices _categoryServices;

        public CoffeItemsController(IRestaurantServices restaurantServices, ICategoryServices categoryServices)
        {
            _restaurantServices = restaurantServices;
            _categoryServices = categoryServices;
        }

        [Authorize(Roles = "manager,restaurant")]
        [HttpGet]
        [Route("/Home/CoffeItems")]
        public IActionResult Index()
        {
            return View();
        }

        // GET: api/<RestaurantController>
        [HttpGet]
        public async Task<List<CoffeItem>> Get()
        {
            return await _restaurantServices.GetCoffeItems();
        }

        // GET api/<RestaurantController>/5
        [HttpGet("{id}")]
        public CoffeItem Get(int id)
        {
            return _restaurantServices.GetCoffeItem(id);
        }

        [Authorize(Roles = "manager,restaurant")]
        // POST api/<RestaurantController>
        [HttpPost]
        public async Task<ActionResult<CoffeItem>> Post([FromBody] CoffeItem value)
        {
            var category = _categoryServices.GetCategory(value.CategoryId);
            if(category == null)
            {
                ModelState.AddModelError("", "This category doesnt exist");
            }
            if (ModelState.IsValid)
            {
                return await _restaurantServices.AddCoffeItem(value);
            }
            return BadRequest();
        }

        [Authorize(Roles = "manager,restaurant")]
        // PUT api/<RestaurantController>/5
        [HttpPut]
        public ActionResult<bool> Put([FromBody] CoffeItem value)
        {
            var category = _categoryServices.GetCategory(value.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError("", "This category doesnt exist");
            }
            if (ModelState.IsValid)
            {
                return _restaurantServices.EditCoffeItem(value);
            }
            return BadRequest();
        }

        [Authorize(Roles = "manager,restaurant")]
        // DELETE api/<RestaurantController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _restaurantServices.DeleteCoffeItem(id);
        }
    }
}
